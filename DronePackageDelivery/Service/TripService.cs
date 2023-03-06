using DronePackageDelivery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DronePackageDelivery.Service
{
    public class TripService : ITripService
    {
        public TripService() {
        }

        public IList<Trip> SearchMinimumBestTrips(IEnumerable<Drone> drones, IEnumerable<Location> locations)
        {
            var dronesSorted = drones.OrderByDescending(x => x.weight);
            locations = locations.OrderBy(x => x.weight);

            var maxCapacityDrone = dronesSorted.First();

            var candidateDrone = maxCapacityDrone.ShallowCopy();

            var sortedDronesQueue = new Queue<Drone>(dronesSorted.Count() > 1 ? dronesSorted.Skip(1) : dronesSorted);

            IEnumerable<Location> remainingLocation = new List<Location>(locations.Where(x => x.weight <= candidateDrone?.weight));

            IList<Trip> listOfTrips = new List<Trip>();

            while (remainingLocation.Any())
            {
                IList<Location> visitedLocations = new List<Location>();

                visitedLocations = Search_Combination(candidateDrone, remainingLocation.ToList());

                var visitedLocationsWeigth = visitedLocations.Sum(x => x.weight);

                if (sortedDronesQueue.Count > 0 && sortedDronesQueue.Peek().weight >= visitedLocationsWeigth)
                {
                    candidateDrone = sortedDronesQueue.Dequeue().ShallowCopy();
                }

                listOfTrips.Add(new Trip()
                {
                    drone = candidateDrone,
                    locations = visitedLocations,
                    totalTrips = visitedLocations.Any() ? visitedLocations.Count() : 0
                });

                remainingLocation = remainingLocation.Except(visitedLocations);
            }

            if(sortedDronesQueue.Count > 0)
            {
                listOfTrips.Add(new Trip()
                {
                    drone = sortedDronesQueue.Dequeue(),
                    locations = new List<Location>()
                });
            }

            var dronesList = drones.ToList();
            return listOfTrips.OrderBy(x => dronesList.IndexOf(x.drone)).ToList();
        }

        private IList<Location> Search_Combination(Drone candidateDrone, IList<Location> remainingLocation)
        {
            List<Location> bestCombination = new List<Location>();
            IList<Location> visitedLocations = new List<Location>();
            int currentDroneCapacity = candidateDrone.weight;
            var highestWeightCapacity = int.MaxValue;

            for (int i = 0; i < remainingLocation.Count; i++)
            {
                var index = visitedLocations.IndexOf(remainingLocation[i]);
                if (index < 0)
                {
                    visitedLocations.Add(new Location()
                    {
                        name = remainingLocation[i].name,
                        weight = remainingLocation[i].weight
                    });
                    currentDroneCapacity = currentDroneCapacity - remainingLocation[i].weight;
                    bestCombination = new List<Location>(visitedLocations);
                }

                for (int j = i + 1; j < remainingLocation.Count; j++)
                {
                    for (int k = j; k < remainingLocation.Count; k++)
                    {
                        if (currentDroneCapacity - remainingLocation[k].weight >= 0)
                        {
                            index = visitedLocations.IndexOf(remainingLocation[k]);
                            if (index < 0)
                            {
                                visitedLocations.Add(new Location()
                                {
                                    name = remainingLocation[k].name,
                                    weight = remainingLocation[k].weight
                                });

                                currentDroneCapacity = currentDroneCapacity - remainingLocation[k].weight;

                                if (highestWeightCapacity > currentDroneCapacity)
                                {
                                    highestWeightCapacity = currentDroneCapacity;
                                    bestCombination = new List<Location>(visitedLocations);
                                }

                                if (currentDroneCapacity == 0) return bestCombination;
                            }
                        }
                        else
                        {
                            visitedLocations = new List<Location>();
                            currentDroneCapacity = candidateDrone.weight;
                            highestWeightCapacity = int.MaxValue;
                            break;
                        }
                    }
                }
            }

            return bestCombination;
        }
    }
}

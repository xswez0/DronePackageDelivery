using DronePackageDelivery.Models;

namespace DronePackageDelivery.Service
{
    public interface ITripService
    {
        IList<Trip> SearchMinimumBestTrips(IEnumerable<Drone> drones, IEnumerable<Location> locations);
    }
}
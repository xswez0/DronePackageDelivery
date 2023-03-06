using DronePackageDelivery.Models;

namespace DronePackageDelivery.Common
{
    public interface IDataSource
    {
        InputData readData(string filePath);

        string printData(IEnumerable<Trip> trips);
    }
}
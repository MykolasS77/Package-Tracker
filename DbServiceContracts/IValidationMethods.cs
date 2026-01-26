
namespace DbServiceContracts
{
    public interface IValidationMethods
    {
        public void CheckIfNull(int id);
        public void ValidateStatusFilterValue(string value);

        //Status updates explanation:
        //      • Created → Can be changed to: Sent, Canceled     
        //      • Sent → Can be changed to: Accepted, Returned, Canceled
        //      • Returned → Can be changed to: Sent, Canceled
        //      • Accepted → Final status, cannot be changed
        //      • Canceled → Final status, cannot be changed
        public void CheckUpdateAvailability(string updateStringRepresentation, int packageId);


    }
}

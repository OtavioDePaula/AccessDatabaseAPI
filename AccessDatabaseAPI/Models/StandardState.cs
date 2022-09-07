namespace AccessDatabaseAPI.Models
{
    public class StandardState
    {
        public int standardStateID { get; set; }
        public string standardState { get; set; }

        public StandardState(int standardStateID_, string standardState_)
        {
            this.standardStateID = standardStateID_;
            this.standardState = standardState_;
        }

        public StandardState()
        {
        }
    }
}
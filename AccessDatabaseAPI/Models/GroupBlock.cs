namespace AccessDatabaseAPI.Models
{
    public class GroupBlock
    {
        public int groupblockID { get; set; }
        public string groupblock { get; set; }

        public GroupBlock(int groupblockID_, string groupblock_)
        {
            this.groupblockID = groupblockID_;
            this.groupblock = groupblock_;
        }

        public GroupBlock()
        {
        }
    }
}
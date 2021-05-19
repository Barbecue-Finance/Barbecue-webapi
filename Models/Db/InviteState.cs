namespace Models.Db
{
    public enum InviteState : uint
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        Canceled = 4
    }
}
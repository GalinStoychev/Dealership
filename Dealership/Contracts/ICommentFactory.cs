namespace Dealership.Contracts
{
    public interface ICommentFactory
    {
        IComment CreateComment(string content);
    }
}

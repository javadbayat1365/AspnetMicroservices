namespace Ordering.Application.Exceptions
{
    public class NotFoundException:ApplicationException
    {
        public NotFoundException(string description):base(description)
        {
        }
        public NotFoundException(string name,object key)
            :base($"The {name} is not founded with {key}")
        {
            
        }
    }
}

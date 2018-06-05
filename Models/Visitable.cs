namespace CircuitMagieDeluxe.Models
{
   public interface Visitable
    {
        int accept(Visitor visitor);
    }
}

namespace CircuitMagieDeluxe.Models
{
    interface Visitable
    {
        int accept(Visitor visitor);
    }
}

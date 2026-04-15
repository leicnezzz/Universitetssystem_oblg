namespace Universitetssystem_oblg;
class Lån
{
    public string Låntaker;
    public string LåntakerType; //student eller ansatt
    public Bøker Bok;
    public DateOnly LånsDato;
    public DateOnly? ReturDato;
}
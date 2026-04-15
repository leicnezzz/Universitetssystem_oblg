using Universitetssystem_oblg;
namespace Universitetssystem_oblg;
class Ansatt : LoggInn
{
    public int AnsattID;
    public string Navn;
    public string Epost;
    public string Stilling;
    public string Avdeling;
    public string Rolle; // faglærer eller biblotekar
    public List<Lån> AktiveLån = new List<Lån>();
}
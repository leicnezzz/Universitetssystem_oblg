using Universitetssystem_oblg;
namespace Universitetssystem_oblg;
class Student : LoggInn
{
    public int StudentID;
    public string Navn;
    public string Epost;
    public List<Kurs> Kurs = new List<Kurs>();
    public List<Lån> AktiveLån = new List<Lån>();
}
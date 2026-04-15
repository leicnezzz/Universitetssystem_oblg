namespace Universitetssystem_oblg;
class Kurs
{
    public string KursKode;
    public int StudiePoeng;
    public string KursNavn;
    public Ansatt Faglærer; //Ansatt objekt
    public List<Bøker> Pensum = new List<Bøker>();
    public int MaksPlasser;
    public List<Student> Deltakere = new List<Student>();
    public Dictionary<int, string> Karakterer = new Dictionary<int, string>(); //StudentID og karakter
}
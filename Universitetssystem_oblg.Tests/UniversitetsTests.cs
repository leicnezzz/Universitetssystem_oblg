using Universitetssystem_oblg;

namespace Universitetssystem_oblg.Tests;

public class UniversitetsTests
{
    [Fact]
    public void OpprettKurs_Duplikat()
    {
        List<Kurs> kursliste = new List<Kurs>();
        Kurs eksisterendeKurs = new Kurs
        {
            KursKode = "IS110",
            KursNavn = "Programmering",
            StudiePoeng = 10,
            MaksPlasser = 100
        };
        kursliste.Add(eksisterendeKurs);

        bool finnesAllerede = kursliste.Any(k => k.KursKode.ToLower() == "is110");
        if (!finnesAllerede)
            kursliste.Add(new Kurs {KursKode = "IS110", KursNavn = "Annet Navn"});
        
        Assert.Equal(1, kursliste.Count);
    }
}
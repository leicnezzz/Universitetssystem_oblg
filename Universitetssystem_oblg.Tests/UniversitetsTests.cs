using Universitetssystem_oblg;

namespace Universitetssystem_oblg.Tests;

public class UniversitetsTests
{
    [Fact]
    public void OpprettKurs_Duplikat()
    {
        var kursliste = new List<Kurs>
        {
            new Kurs {KursKode = "IS110", KursNavn = "Programmering", }
        };
        
        string nyKursKode = "IS110";
        bool finnesAllerede = kursliste.Any(k => k.KursKode == nyKursKode);
        if (!finnesAllerede)
        {
            kursliste.Add(new Kurs {KursKode = nyKursKode});
        }
        Assert.Single(kursliste);
    }

    [Fact]
    public void MeldPåKurs_Duplikat()
    {
        var student = new Student { StudentID = 1, Navn = "Kari Nordmann" };
        var kurs = new Kurs
        {
            KursKode = "IS110",
            MaksPlasser = 2,
            Deltakere = new List<Student>()
        };
        
        if (!kurs.Deltakere.Contains(student))
        { 
            kurs.Deltakere.Add(student);
        }
        
        if (!kurs.Deltakere.Contains(student))
        { 
            kurs.Deltakere.Add(student);
        }
        
        Assert.Single(kurs.Deltakere);

    }

    [Fact]
    public void LånOgRetur_Duplikat()
    {
        var bok = new Bøker
        {
            BokID = 1,
            Tittel = "C#",
            TilgjengeligeEksemplarer = 3
        };
        var låneHistorikk = new List<Lån>();

        var lån = new Lån
        {
            Bok = bok,
            Låntaker = "Kari Nordmann",
            LånsDato = DateOnly.FromDateTime(DateTime.Now),
        };
        
        bok.TilgjengeligeEksemplarer--;
        låneHistorikk.Add(lån);
        
        Assert.Equal(2, bok.TilgjengeligeEksemplarer);
        
        lån.ReturDato = DateOnly.FromDateTime(DateTime.Now);
        bok.TilgjengeligeEksemplarer++;
        
        Assert.Equal(3, bok.TilgjengeligeEksemplarer);
        
    }
}

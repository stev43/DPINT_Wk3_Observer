using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Model
{
    public class Aankomsthal : IObserver<Baggageband>
    {
        // TODO: Hier een ObservableCollection van maken, dan weten we wanneer er vluchten bij de wachtrij bij komen of afgaan.
        public ObservableCollection<Vlucht> WachtendeVluchten { get; private set; }
        public List<Baggageband> Baggagebanden { get; private set; }

        public Aankomsthal()
        {
            WachtendeVluchten = new ObservableCollection<Vlucht>();
            Baggagebanden = new List<Baggageband>();

            // TrODO: Als baggageband Observable is, gaan we subscriben op band 1 zodat we updates binnenkrijgen.
            Baggageband band = new Baggageband("Band 1", 30);
            band.Subscribe(this);
            Baggagebanden.Add(band);
            // TrODO: Als baggageband Observable is, gaan we subscriben op band 2 zodat we updates binnenkrijgen.
            band = new Baggageband("Band 2", 60);
            band.Subscribe(this);
            Baggagebanden.Add(band);
            // TrODO: Als baggageband Observable is, gaan we subscriben op band 3 zodat we updates binnenkrijgen.
            band = new Baggageband("Band 3", 90);
            band.Subscribe(this);
            Baggagebanden.Add(band);
        }

        public void NieuweInkomendeVlucht(string vertrokkenVanuit, int aantalKoffers)
        {
            if (Baggagebanden.Any(b => b.AantalKoffers == 0))
            {
                Baggageband legeBand = Baggagebanden.FirstOrDefault(b => b.AantalKoffers == 0);
                legeBand.HandelNieuweVluchtAf(new Vlucht(vertrokkenVanuit, aantalKoffers));
            }
            else
            {
                WachtendeVluchten.Add(new Vlucht(vertrokkenVanuit, aantalKoffers));
            }
            // TODO: Het proces moet straks automatisch gaan, dus als er lege banden zijn moet de vlucht niet in de wachtrij.
            // Dan moet de vlucht meteen naar die band.

            // Denk bijvoorbeeld aan: Baggageband legeBand = Baggagebanden.FirstOrDefault(b => b.AantalKoffers == 0);
        }

        public void WachtendeVluchtenNaarBand(Baggageband band)
        {
            if (band.AantalKoffers == 0 && WachtendeVluchten.Any())
            {
                Vlucht volgendeVlucht = WachtendeVluchten.FirstOrDefault();
                volgendeVlucht.StopWaiting();
                WachtendeVluchten.RemoveAt(0);
                band.HandelNieuweVluchtAf(volgendeVlucht);
            }
        }

        /// <summary>
        /// Deze gaan we niet gebruiken
        /// </summary>
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deze gaan we niet gebruiken
        /// </summary>
        /// <param name="error"></param>
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Als er een update is wordt deze aangeroepen, je krijgt hier heel het object
        /// binnen.Dus elke keer als er een waarde binnen het object dat wij in de gaten houden
        /// verandert zal deze methode aangeroepen worden.We kunnen dan onze view aansturen dat
        /// de nieuwe waarde op het scherm moet komen.
        /// </summary>
        /// <param name="value"></param>
        public void OnNext(Baggageband value)
        {
            WachtendeVluchtenNaarBand(value);
        }
    }
}

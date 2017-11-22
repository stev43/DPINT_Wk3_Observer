using DPINT_Wk3_Observer.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.ViewModel
{
    public class BaggagebandViewModel : ViewModelBase, IObserver<Baggageband>
    {
        private string _vluchtVertrokkenVanuit;
        public string VluchtVertrokkenVanuit
        {
            get { return _vluchtVertrokkenVanuit; }
            set
            {
                _vluchtVertrokkenVanuit = value;
                RaisePropertyChanged("VluchtVertrokkenVanuit");
            }
        }

        private int _aantalKoffers;
        public int AantalKoffers
        {
            get { return _aantalKoffers; }
            set
            {
                _aantalKoffers = value;
                RaisePropertyChanged("AantalKoffers");
            }
        }

        private string _naam;
        public string Naam
        {
            get { return _naam; }
            set
            {
                _naam = value;
                RaisePropertyChanged("Naam");
            }
        }

        private IDisposable _unsubscriber;

        public BaggagebandViewModel(Baggageband band)
        {
            _unsubscriber = band.Subscribe(this);
            ((IObserver<Baggageband>)this).OnNext(band);
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
            VluchtVertrokkenVanuit = value.VluchtVertrokkenVanuit;
            AantalKoffers = value.AantalKoffers;
            Naam = value.Naam;
        }
    }
}

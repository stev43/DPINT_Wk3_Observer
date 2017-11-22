using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Model
{
    public class Observable<T> : IObservable<T>, IDisposable where T : class
    {
        private List<IObserver<T>> _observerList;
        public List<IObserver<T>> ObserverList
        {
            get { return _observerList; }
            set { _observerList = value; }
        }

        public Observable()
        {
            _observerList = new List<IObserver<T>>();
        }

        private struct Unsubscriber : IDisposable
        {
            private Action _unsubscribe;
            public Unsubscriber(Action unsubscribe) { _unsubscribe = unsubscribe; }
            public void Dispose() { _unsubscribe(); }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observerList.Add(observer);
            // TODO: We moeten bijhouden wie ons in de gaten houdt.
            // TODO: Stop de observer dus in de lijst met observers. We weten dan
            // welke objecten we allemaal een seintje moeten geven.
            // Daarna geven we een object terug.
            // Als dat object gedisposed wordt geven wij
            // de bovenstaande observer geen seintjes meer.
            return new Unsubscriber(() => _observerList.Remove(observer));
        }

        protected virtual void Notify()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

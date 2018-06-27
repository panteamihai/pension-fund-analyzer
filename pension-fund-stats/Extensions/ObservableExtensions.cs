using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace PensionAnalysis.Extensions
{
    public static class ObservableExtensions
    {
        public static IObservable<Unit> ToUnit<T>(this IObservable<T> source)
        {
            return source.Select(_ => Unit.Default);
        }
    }
}

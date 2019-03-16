using Lab.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }

            //foreach (var product in source)
            //{
            //    if (predicate(product))
            //    {
            //        yield return product;
            //    }
            //}
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> sources, Func<TSource, int, bool> predicate)
        {
            var index = 0;
            foreach (var source in sources)
            {
                if (predicate(source, index))
                {
                    yield return source;
                }

                index++;
            }
        }

        public static IEnumerable<TResult> JoeySelectWithSeqNo<TSource, TResult>(this IEnumerable<TSource> sources, Func<TSource, int, TResult> mapper)
        {
            int index = 1;
            foreach (var source in sources)
            {
                yield return mapper(source, index);
                index++;
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> sources, Func<TSource, TResult> mapper)
        {
            foreach (var source in sources)
            {
                yield return mapper(source);
            }
        }

        public static IEnumerable<TSource> JoeyTake<TSource>(this IEnumerable<TSource> employees, int count)
        {
            var enumerator = employees.GetEnumerator();
            int index = 0;
            while (index < count && enumerator.MoveNext())
            {
                yield return enumerator.Current;
                index++;
            }
        }

        public static IEnumerable<TSource> JoeySkip<TSource>(this IEnumerable<TSource> employees, int count)
        {
            var enumerator = employees.GetEnumerator();
            int index = 0;
            while (enumerator.MoveNext())
            {
                if (index >= count)
                {
                    yield return enumerator.Current;
                }
                index++;
            }
        }

        public static IEnumerable<int> JoeyGroupSum<TSource>(this IEnumerable<TSource> products, int size, Func<TSource, int> selector)
        {
            var pageSize = size;
            var pageIndex = 0;
            while (products.Count() >= pageSize * pageIndex)
            {
                yield return products.JoeySkip(pageIndex * pageSize).JoeyTake(pageSize).Sum(selector);
                pageIndex++;
            }
        }

        public static bool JoeyAll(this IEnumerable<Girl> girls, Func<IEnumerator<Girl>, bool> predicate)
        {
            var enumerator = girls.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator))
                {
                    return false;
                }
            }
            return true;
        }

        public static TSource JoeyFirstOrDefault<TSource>(this IEnumerable<TSource> employees)
        {
            var enumerator = employees.GetEnumerator();
            return enumerator.MoveNext() ? enumerator.Current : default(TSource);
        }

        public static TSource JoeyLastOrDefault<TSource>(this IEnumerable<TSource> employees, Func<TSource, bool> predicate)
        {
            TSource result = default(TSource);
            var enumerator = employees.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var source = enumerator.Current;
                if (predicate(source))
                {
                    result = enumerator.Current;
                }
            }

            return result;
        }

        public static IEnumerable<TSource> JoeyReverse<TSource>(this IEnumerable<TSource> sources)
        {
            return new Stack<TSource>(sources);
            //Stack<TSource> stackEmployee = new Stack<TSource>();
            //var enumerator = sources.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    stackEmployee.Push(enumerator.Current);
            //}

            //return stackEmployee;
        }
    }
}
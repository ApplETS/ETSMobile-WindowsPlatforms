using Ets.Mobile.Client.Factories.Interfaces.Moodle;
using Ets.Mobile.Client.Factories.Interfaces.Shared;
using System;
using System.Reflection;

namespace Ets.Mobile.Client.Factories.Abstractions
{
    public abstract class MoodleAbstractFactory
    {
        public abstract IMoodleSiteInfoFactory GetMoodleSiteInfoFactory();
        public abstract IMoodleCourseFactory GetMoodleCourseFactory();
        public abstract IMoodleCourseContentFactory GetMoodleCourseContentFactory();
    }

    public static class MoodleFactoryExtensions
    {
        /// <summary>
        /// <para>Create an object using the <param name="abstractFactory">Abstract Factory</param></para>
        /// <para>The factory is resolved by type using Reflection</para>
        /// </summary>
        /// <typeparam name="TInput">Result</typeparam>
        /// <typeparam name="TOutput">View Model</typeparam>
        /// <param name="abstractFactory">Abstract Factory</param>
        /// <param name="result">Result</param>
        /// <returns></returns>
        public static TOutput CreateFor<TInput, TOutput>(this MoodleAbstractFactory abstractFactory, TInput result)
        {
            var runtimeMethod = abstractFactory.GetType()
                    .GetRuntimeMethod(FormatGetFactory(typeof(TInput)), new Type[] { });

            var factory = (IFactory<TInput, TOutput>)runtimeMethod.Invoke(abstractFactory, new object[] { });

            return factory.Create(result);
        }

        private const string GetTFactory = "Get{0}Factory";
        private static readonly Func<Type, string> FormatGetFactory = type => string.Format(GetTFactory, type.Name.Replace("[]", ""));
    }
}
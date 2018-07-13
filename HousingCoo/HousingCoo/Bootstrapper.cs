using System;
using Xamarin.Presentation.Navigation;

namespace HousingCoo {
    public class Bootstrapper {
        public static Bootstrapper Instance => bootstrapper.Value;
        static readonly Lazy<Bootstrapper> bootstrapper;
        static Bootstrapper() {
            bootstrapper = new Lazy<Bootstrapper>(()=>new Bootstrapper());

            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap(typeof(Domain.Entities.VotingEntity), typeof(Domain.Model.VotingModel));
            });
        }


        public HostingDResolver Resolver { get; }
        Bootstrapper() {
            Resolver = new HostingDResolver();
        }

    }
}

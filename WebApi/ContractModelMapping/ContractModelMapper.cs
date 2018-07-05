using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;
using Payoneer.Payoneer.Hotels.Contracts;
//using Payoneer.Payoneer.Hotels.Model.ExampleDomain;
//using Payoneer.Payoneer.Hotels.Model.ExampleDomain2;
using Payoneer.Payoneer.Hotels.Model.HotelsDomain;

namespace Payoneer.Payoneer.Hotels.WebApi.ContractModelMapping
{
    /// <summary>
    /// Map contract to model and vice versa
    /// </summary>
    public static class ContractModelMapper
    {
        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static Customer ToModel(this CustomerCI contract)
        {
            if (contract == null)
                return null;

            var result = new Customer();
            result.InjectFrom(contract);

            // Map any non-identical properties here
            //result.LookupDataId = LookupDataNameById(contract.LookupName);

            return result;
        }

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CustomerCI ToContract(this Customer model)
        {
            if (model == null)
                return null;

            var result = new CustomerCI();
            result.InjectFrom(model);

            // Map any non-identical properties here
            //result.LookupName = LookupDataNameById(model.LookupDataId);

            return result;
        }

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IList<CustomerCI> ToContract(this IList<Customer> model)
        {
            if (model == null)
                return null;

            var modelList = model.ToList();
            var result = modelList.Select(r => new CustomerCI().InjectFrom(r)).Cast<CustomerCI>().ToList();

            return result;
        }

        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static IList<Customer> ToModel(this IList<CustomerCI> contract)
        {
            if (contract == null)
                return null;

            var contractList = contract.ToList();
            var result = contractList.Select(r => new Customer().InjectFrom(r)).Cast<Customer>().ToList();

            return result;
        }

        /*========================================*/

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static HotelCI ToContract(this Hotel model)
        {
            if (model == null)
                return null;

            var result = new HotelCI();
            result.InjectFrom(model);

            // Map any non-identical properties here
            //result.LookupName = LookupDataNameById(model.LookupDataId);

            return result;
        }

        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static Hotel ToModel(this HotelCI contract)
        {
            if (contract == null)
                return null;

            var result = new Hotel();
            result.InjectFrom(contract);

            // Map any non-identical properties here
            //result.LookupDataId = LookupDataNameById(contract.LookupName);

            return result;
        }

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IList<HotelCI> ToContract(this IList<Hotel> model)
        {
            if (model == null)
                return null;

            var modelList = model.ToList();
            var result = modelList.Select(r => new HotelCI().InjectFrom(r)).Cast<HotelCI>().ToList();

            return result;
        }

        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static IList<Hotel> ToModel(this IList<HotelCI> contract)
        {
            if (contract == null)
                return null;

            var contractList = contract.ToList();
            var result = contractList.Select(r => new Hotel().InjectFrom(r)).Cast<Hotel>().ToList();

            return result;
        }

        /*========================================*/

        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static Reservation ToModel(this ReservationCI contract)
        {
            if (contract == null)
                return null;

            var result = new Reservation();
            result.InjectFrom(contract);

            // Map any non-identical properties here
            //result.LookupDataId = LookupDataNameById(contract.LookupName);

            return result;
        }

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ReservationCI ToContract(this Reservation model)
        {
            if (model == null)
                return null;

            var result = new ReservationCI();
            result.InjectFrom(model);

            // Map any non-identical properties here
            //result.LookupName = LookupDataNameById(model.LookupDataId);

            return result;
        }

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IList<ReservationCI> ToContract(this IList<Reservation> model)
        {
            if (model == null)
                return null;

            var modelList = model.ToList();
            var result = modelList.Select(r => new ReservationCI().InjectFrom(r)).Cast<ReservationCI>().ToList();

            return result;
        }

        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static IList<Reservation> ToModel(this IList<ReservationCI> contract)
        {
            if (contract == null)
                return null;

            var contractList = contract.ToList();
            var result = contractList.Select(r => new Reservation().InjectFrom(r)).Cast<Reservation>().ToList();

            return result;
        }

        /*========================================*/

        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static Room ToModel(this RoomCI contract)
        {
            if (contract == null)
                return null;

            var result = new Room();
            result.InjectFrom(contract);

            result.RoomBeds = contract.RoomBeds.Select(b => new RoomBed().InjectFrom(b)).Cast<RoomBed>().ToList();

                //result[i].Reservations = modelList[i].Reservations.Select(
                //    b => new ReservationCI().InjectFrom(b)).Cast<ReservationCI>().ToList();

            return result;
        }

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static RoomCI ToContract(this Room model)
        {
            if (model == null)
                return null;

            var result = new RoomCI();
            result.InjectFrom(model);

            // Map any non-identical properties here
            //result.LookupName = LookupDataNameById(model.LookupDataId);

            return result;
        }

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IList<RoomCI> ToContract(this IList<Room> model)
        {
            if (model == null)
                return null;

            var modelList = model.ToList();
            var result = modelList.Select(r => new RoomCI().InjectFrom(r)).Cast<RoomCI>().ToList();
            for (var i = 0; i < modelList.Count; i++)
            {
                result[i].RoomBeds = modelList[i].RoomBeds.Select(
                        b => new RoomBedCI().InjectFrom(b)).Cast<RoomBedCI>().ToList();

                result[i].Reservations = modelList[i].Reservations.Select(
                        b => new ReservationCI().InjectFrom(b)).Cast<ReservationCI>().ToList();
            }

            return result;
        }

        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static IList<Room> ToModel(this IList<RoomCI> contract)
        {
            if (contract == null)
                return null;

            var contractList = contract.ToList();
            var result = contractList.Select(r => new Room().InjectFrom(r)).Cast<Room>().ToList();
            for (var i = 0; i < contractList.Count; i++)
            {
                result[i].RoomBeds = contractList[i].RoomBeds.Select(
                    b => new RoomBed().InjectFrom(b)).Cast<RoomBed>().ToList();

                result[i].Reservations = contractList[i].Reservations.Select(
                    b => new Reservation().InjectFrom(b)).Cast<Reservation>().ToList();
            }

            return result;
        }

        /*========================================*/

        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static RoomBed ToModel(this RoomBedCI contract)
        {
            if (contract == null)
                return null;

            var result = new RoomBed();
            result.InjectFrom(contract);

            // Map any non-identical properties here
            //result.LookupDataId = LookupDataNameById(contract.LookupName);

            return result;
        }

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static RoomBedCI ToContract(this RoomBed model)
        {
            if (model == null)
                return null;

            var result = new RoomBedCI();
            result.InjectFrom(model);

            // Map any non-identical properties here
            //result.LookupName = LookupDataNameById(model.LookupDataId);

            return result;
        }

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IList<RoomBedCI> ToContract(this IList<RoomBed> model)
        {
            if (model == null)
                return null;

            var modelList = model.ToList();
            var result = modelList.Select(r => new RoomBedCI().InjectFrom(r)).Cast<RoomBedCI>().ToList();

            return result;
        }

        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static IList<RoomBed> ToModel(this IList<RoomBedCI> contract)
        {
            if (contract == null)
                return null;

            var contractList = contract.ToList();
            var result = contractList.Select(r => new RoomBed().InjectFrom(r)).Cast<RoomBed>().ToList();

            return result;
        }

        /***********************************************************************************************************

        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static ExampleMessageModel ToModel(this ExampleMessage contract)
        {
            if (contract == null)
                return null;

            var result = new ExampleMessageModel();
            result.InjectFrom(contract);

            return result;
        }

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ExampleMessage ToContract(this ExampleMessageModel model)
        {
            if (model == null)
                return null;

            var result = new ExampleMessage();
            result.InjectFrom(model);

            return result;
        }


        /// <summary>
        /// Maps contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static ExampleContract ToContract(this ExampleModel contract)
        {
            if (contract == null)
                return null;

            var result = new ExampleContract();
            result.InjectFrom(contract);

            return result;
        }

        /// <summary>
        /// Maps model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ExampleModel ToModel(this ExampleContract model)
        {
            if (model == null)
                return null;

            var result = new ExampleModel();
            result.InjectFrom(model);

            return result;
        }


       [Cache("localCacheDictionaries")]
        private static string LookupDataNameById(short? id)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginOuterLockFree();

                string result;
                using (var context = DiResolver.Resolve<IExampleDomainContext>())
                {
                    result = context.LookupData.AsQueryable()
                        .Where(d => d.Id == id).Select(d => d.Name).FirstOrDefault();
                }

                return result;
            }
        }

        [Cache("localCacheDictionaries")]
        private static short? LookupDataNameById(string name)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginOuterLockFree();

                short? result;
                using (var context = DiResolver.Resolve<IExampleDomainContext>())
                {
                    result = context.LookupData.AsQueryable()
                        .Where(d => d.Name == name).Select(d => (short?)d.Id).FirstOrDefault();
                }

                return result;
            }
        }
        */
    }
}
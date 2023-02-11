using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Firma.Models.BusinessLogic
{
    public class TheBestB : DatabaseClass
    {
        #region Konstruktor
        public TheBestB(JJFirmaEntities Db)
            : base(Db)
        {
        }
        #endregion
        #region FunkcjeBiznesowe
        public string TheBestOf(int TheBestId, DateTime dataOd, DateTime dataDo)
        {
            switch (TheBestId)
            {
                case 0:
                    return mostPopularProduct(dataOd, dataDo);
                case 1:
                    return highestPaidEmployee(dataOd, dataDo);
                case 2:
                    return mostEmployeesInTheJobPosition(dataOd, dataDo).ToLower();
                default: return null;
            }
        }
        private string mostPopularProduct(DateTime dataOd, DateTime dataDo)
        {
            try
            {
                var wz = Db.WydaniaZewnetrzne.Where(x => x.DataWydania >= dataOd && x.DataWydania <= dataDo && x.CzyAktywny == true).Select(x => x.WydanieZewnetrzneId).ToList();
                var najpopularniejszyTowar = Db.PozycjeWydaniaZewnetrznego
                    .Where(x => wz.Contains(x.WydanieZewnetrzneId) && x.CzyAktywny == true)
                    .GroupBy(x => x.TowarId)
                    .Select(g => new { TowarId = g.Key, Ilosc = g.Sum(x => x.Ilosc) })
                    .OrderByDescending(x => x.Ilosc)
                    .First();
                return Db.Towary.Where(x => x.TowarId == najpopularniejszyTowar.TowarId).Select(x => x.Nazwa).FirstOrDefault();
            }
            catch (Exception) { return null; }
        }
        private string highestPaidEmployee(DateTime dataOd, DateTime dataDo)
        {
            try
            {
                var przypisaneUmowy = Db.PracownicyUmowy.Where(x => x.CzyAktywny == true).Select(x => x.UmowaId).ToList();
                var umowa = Db.Umowy
                    .Where(x => x.DataOd < dataDo && dataOd < x.DataDo && x.CzyAktywny == true && przypisaneUmowy.Contains(x.UmowaId))
                    .OrderByDescending(u => u.StawkaBruttoMies + u.StawkaBruttoGodz * u.CzasPracyMies).FirstOrDefault();
                var pracownikId = Db.PracownicyUmowy.Where(x => x.UmowaId == umowa.UmowaId).Select(x => x.PracownikId).FirstOrDefault();
                var pracownik = Db.Pracownicy.Where(x => x.PracownikId == pracownikId).FirstOrDefault();
                return pracownik.Imie + " " + pracownik.Nazwisko;
            }
            catch (Exception) { return null; }
        }
        private string mostEmployeesInTheJobPosition(DateTime dataOd, DateTime dataDo)
        {
            try
            {
                var przypisaneUmowy = Db.PracownicyUmowy.Where(x => x.CzyAktywny == true).Select(x => x.UmowaId).ToList();
                var stanowiskaId = Db.Umowy
                        .Where(x => x.DataOd < dataDo && dataOd < x.DataDo && x.CzyAktywny == true && przypisaneUmowy.Contains(x.UmowaId))
                        .GroupBy(u => u.StanowiskoId)
                        .OrderByDescending(g => g.Count())
                        .Select(g => g.Key).ToList();
                var stanowiska = Db.UmowyStanowiska.Where(x => stanowiskaId.Contains(x.StanowiskoId)).Select(x => x.Nazwa).ToList();
                string nazwy = "";
                foreach (var stanowisko in stanowiska)
                    nazwy += stanowisko+"\n";
                    return nazwy.Trim();
            }
            catch (Exception) { return null; }
        }
        #endregion

    }
}

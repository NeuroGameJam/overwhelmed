using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts
{
    public class GameManagerScript : MonoBehaviour
    {

        private DateTime _startDate;

        public List<DateTime> InfinityRunnerFails = new List<DateTime>();
        public List<DateTime> TimesJumped = new List<DateTime>();
        public int TotalCubesOnInfinityRunner;

        public List<DateTime> TurnMeOffFails = new List<DateTime>();
        public List<DateTime> TurnMeOffCorrects = new List<DateTime>();

        public List<DateTime> FlyingFails = new List<DateTime>();
        public int TotalFlyingEnemies;

        public List<DateTime> MemoryFails = new List<DateTime>();
        public List<DateTime> MemoryCorrects = new List<DateTime>();

        public int initial;
        [Header("Screens: ")]
        [Header("Puzzle 1")]
        public GameObject puzzle1;
        public GameObject puzzle1TVON;
        public GameObject puzzle1TVOFF;
        public int wait1;
        [Header("Puzzle 2")]
        public GameObject puzzle2;
        public GameObject puzzle2TVON;
        public GameObject puzzle2TVOFF;
        public int wait2;
        [Header("Puzzle 3")]
        public GameObject puzzle3;
        public GameObject puzzle3TVON;
        public GameObject puzzle3TVOFF;
        public int wait3;
        [Header("Puzzle 4")]
        public GameObject puzzle4;
        public GameObject puzzle4TVON;
        public GameObject puzzle4TVOFF;

        public int lastwait;

        public GameObject Result;
        public TextMesh TextResult;


        // Use this for initialization
        public void Start ()
        {
            //_startDate = DateTime.Now;
            StartCoroutine(STARTER());
        }

        IEnumerator STARTER()
        {
            Debug.Log("");
            yield return new WaitForSeconds(initial);
            _startDate = DateTime.Now;
            puzzle1TVON.SetActive(true);
            puzzle1TVOFF.SetActive(false);
            puzzle1.GetComponent<InfinityRunnerGame>().enabled = true;
            yield return new WaitForSeconds(wait1);
            puzzle2TVON.SetActive(true);
            puzzle2TVOFF.SetActive(false);
            puzzle2.GetComponent<TurnMeOffScript>().enabled = true;
            yield return new WaitForSeconds(wait2);
            puzzle3TVON.SetActive(true);
            puzzle3TVOFF.SetActive(false);
            yield return new WaitForSeconds(5);
            puzzle3.GetComponent<FlyingGame>().enabled = true;
            yield return new WaitForSeconds(wait3);
            puzzle4TVON.SetActive(true);
            puzzle4TVOFF.SetActive(false);
            puzzle4.GetComponent<MemoryGame>().enabled = true;
            //the wait while the player is playing all the games at the same time. Good luck to him!
            yield return new WaitForSeconds(lastwait);

            //End game
            //TODO change screens to gameover
            puzzle1TVON.SetActive(false);
            puzzle1TVOFF.SetActive(true);

            puzzle2TVON.SetActive(false);
            puzzle2TVOFF.SetActive(true);

            puzzle3TVON.SetActive(false);
            puzzle3TVOFF.SetActive(true);

            puzzle4TVON.SetActive(false);
            puzzle4TVOFF.SetActive(true);

            puzzle1.GetComponent<InfinityRunnerGame>().enabled = false;
            puzzle2.GetComponent<TurnMeOffScript>().enabled = false;
            puzzle3.GetComponent<FlyingGame>().enabled = false;
            puzzle4.GetComponent<MemoryGame>().enabled = false;

            ExportData();

            var memoryperc = MemoryCorrects.Count + MemoryFails.Count == 0 ? 0 : MemoryCorrects.Count / (MemoryCorrects.Count + MemoryFails.Count) * 100;
            var flyingperc = TotalFlyingEnemies == 0 ? 0 : (TotalFlyingEnemies - FlyingFails.Count) / TotalFlyingEnemies * 100;
            var turnmeoffperc = (TurnMeOffFails.Count + TurnMeOffCorrects.Count) == 0 ? 0 : TurnMeOffCorrects.Count / (TurnMeOffFails.Count + TurnMeOffCorrects.Count) * 100;
            var infiniterunnerperc = TotalCubesOnInfinityRunner == 0 ? 0 : (TotalCubesOnInfinityRunner - InfinityRunnerFails.Count) / TotalCubesOnInfinityRunner * 100;
            TextResult.text = "Results:\nJumping: " + infiniterunnerperc + "%\nRed button: " + turnmeoffperc + "%\nFlying: " + flyingperc + "%\nMemory:" + memoryperc + "%";
            Result.gameObject.SetActive(true);
        }

        public void AddDataToInfinityRunner(bool good)
        {
            if (good)
                TimesJumped.Add(DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond)));
            else
                InfinityRunnerFails.Add(DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond)));
        }

        public void AddCubeOnInfinityRunner()
        {
            TotalCubesOnInfinityRunner++;
        }

        public void AddDataToButtonToTurnOff(bool good)
        {
            if (good)
                TurnMeOffFails.Add(DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond)));
            else
                TurnMeOffCorrects.Add(DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond)));
        }

        public void AddDataToFlyingFails()
        {
            FlyingFails.Add(DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond)));
        }

        public void AddCubeOnFlyingGame()
        {
            TotalFlyingEnemies++;
        }

        public void AddDataToMemoryGame(bool good)
        {
            if (good)
                MemoryCorrects.Add(DateTime.Now.AddTicks(-(DateTime.Now.Ticks%TimeSpan.TicksPerSecond)));
            else
                MemoryFails.Add(DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond)));
        }

        public void ExportData()
        {
            var endDate = DateTime.Now;
            var allDates = new List<DateTime>();

            _startDate = _startDate.AddTicks(-(_startDate.Ticks % TimeSpan.TicksPerSecond));
            endDate = endDate.AddTicks(-(endDate.Ticks % TimeSpan.TicksPerSecond));

            using (var file = new System.IO.StreamWriter(@"C:\Results.txt"))
            {
                var currentDate = _startDate + new TimeSpan(0,0,1);
                while (currentDate < endDate)
                {
                    allDates.Add(currentDate + new TimeSpan(0));
                    currentDate = currentDate + new TimeSpan(0, 0, 1);
                }
                
                file.WriteLine("Times,Infinite Runner Fails,Infinite Runner Jumps,Light Button Corrects,Light Button Fails,Flying Fails,Total Enemies On Flying,Memory Fails,Memory Corrects");
                foreach (var dateTime in allDates)
                {
                    var irf = InfinityRunnerFails.Any(d => d.Equals(dateTime)) ? "1" : "0";
                    var tj = TimesJumped.Any(d => d.Equals(dateTime)) ? "1" : "0";
                    var lbc = TurnMeOffCorrects.Any(d => d.Equals(dateTime)) ? "1" : "0";
                    var lbf = TurnMeOffFails.Any(d => d.Equals(dateTime)) ? "1" : "0";
                    var ff = FlyingFails.Any(d => d.Equals(dateTime)) ? "1" : "0";
                    var mf = MemoryFails.Any(d => d.Equals(dateTime)) ? "1" : "0";
                    var mc = MemoryCorrects.Any(d => d.Equals(dateTime)) ? "1" : "0";

                    file.WriteLine(dateTime + "," + irf + "," + tj + "," + lbc + "," + lbf + "," + ff + "," + mf + "," + mc);
                }

                file.WriteLine("Total Enemies On Infinite Runner: " + TotalCubesOnInfinityRunner);
                file.WriteLine("Total Enemies On Flying: " + TotalFlyingEnemies);

                file.WriteLine(InfinityRunnerFails.Count);
            }
        }
    }
}

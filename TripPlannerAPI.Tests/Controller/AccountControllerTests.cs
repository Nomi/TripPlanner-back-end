using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlannerAPI.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using TripPlannerAPI.Services;
using TripPlannerAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using Microsoft.Extensions.Logging.Abstractions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TripPlannerAPI.DTOs.AccountDTOs;
using TripPlannerAPI.Repositories;

namespace TripPlannerAPI.Tests.Controller
{
    public static class BalanceSettler
    {
        public static void TestRun() //Name it Main() for HackerRank CodePair
        {
            Dictionary<int, int> inputVals = new();
            Console.WriteLine("-=================-");

            //Part 1: Sample1 
            inputVals.Add(0, 0);
            inputVals.Add(1, 0);
            inputVals.Add(2, 0);
            inputVals.Add(3, 0);
            List<List<int>> part1List = new() { new() { 0, 1, 100 }, new() { 2, 3, 200 }, new() { 3, 1, 50 } };
            Dictionary<int, int> resultPart1 = getNewBalances(part1List, inputVals);
            foreach (int key in resultPart1.Keys)
            {
                Console.WriteLine(key.ToString() + ": " + resultPart1[key].ToString());
            }


            inputVals.Clear();
            //Part 2: Sample 1
            Console.WriteLine("-=================-");
            inputVals.Add(0, -100);
            inputVals.Add(1, 150);
            inputVals.Add(2, -200);
            inputVals.Add(3, 150);
            List<List<int>> lists = settleBalances(inputVals);
            foreach (var list in lists)
            {
                Console.Write("[");
                foreach (var num in list)
                {
                    Console.Write(num.ToString() + ", ");
                }
                Console.WriteLine("]");
            }

            inputVals.Clear();

            //Part 2: Sample 2
            Console.WriteLine("-=================-");
            inputVals.Add(0, -800);
            inputVals.Add(1, -500);
            inputVals.Add(2, -100);
            inputVals.Add(3, -100);
            inputVals.Add(4, 100);
            inputVals.Add(5, 100);
            inputVals.Add(6, 1300);
            List<List<int>> lists2 = settleBalances(inputVals); ;
            foreach (var list in lists2)
            {
                Console.Write("[");
                foreach (var num in list)
                {
                    Console.Write(num.ToString() + ", ");
                }
                Console.WriteLine("]");
            }

            return;
        }
        public static Dictionary<int, int> getNewBalances(List<List<int>> transactionList, Dictionary<int, int> startingBalance)
        {
            //Copy existing values to result
            Dictionary<int, int> result = new(startingBalance);

            //Go over transactions and calculate result
            for (int i = 0; i < transactionList.Count; i++) //[O(N)]
            {
                result[transactionList[i][0]] -= transactionList[i][2];
                result[transactionList[i][1]] += transactionList[i][2];
            }

            return result;
        }

        public static List<List<int>> settleBalances(Dictionary<int, int> customersToAmount)
        {
            List<List<int>> result = new();

            List<int> keys = new();
            List<int> values = new();
            foreach (KeyValuePair<int, int> tran in customersToAmount.OrderBy(kvp => kvp.Value))
            {
                keys.Add(tran.Key);
                values.Add(tran.Value);
            }



            for (int i = 0, j = values.Count - 1; i != j;)
            {
                if (i == j)
                    break;
                int paymentMade = (int)Math.Min(Math.Abs(values[i]), values[j]);//calculate the minimum payment that can be made.
                result.Add(new List<int> { keys[j], keys[i], paymentMade }); //make the min payment.
                if (paymentMade == (int)Math.Abs(values[i]))//if fully paid.
                {
                    values[j] -= paymentMade;
                    values[i] += paymentMade;
                    i++; //move to next biggest lender
                    if (i == j)
                        break;
                    if (values[j] == 0) //j's balance is settled.
                    {
                        j--; //move to next biggest borrower
                    }
                    continue;
                }
                else if (paymentMade != values[i]) //not enough money to pay, i's balance not fully settled.
                {
                    values[j] -= paymentMade;
                    values[i] += paymentMade;
                    j--; //move to next biggest borrower
                }
            }

            return result;
        }
    }
}

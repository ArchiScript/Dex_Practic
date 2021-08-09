﻿using System;
using System.Collections.Generic;
using System.Text;
using BankSystem.Models;
using System.Linq;
using BankSystem.Services;

namespace BankSystem.Services
{
    public class BankServices
    {
        public static List<Client> clients = new List<Client>();
        public static List<Employee> employees = new List<Employee>();
        public static Dictionary<Client, List<Account>> clientsAcc = new Dictionary<Client, List<Account>>();

        public delegate decimal ExchangeDelegate(decimal sum, Currency convertFrom, Currency convertTo );

        public  void Add<T>(T person) where T : Person
        {
            if (person is Client)
            {
                var client = person as Client;
                clients.Add(new Client
                {
                    Name = client.Name,
                    PassNumber = client.PassNumber,
                    DateOfBirth = client.DateOfBirth,
                    Id = client.Id,

                });
            }
            else
            {
                var employee = person as Employee;
                employees.Add(new Employee
                {
                    Name = employee.Name,
                    PassNumber = employee.PassNumber,
                    DateOfBirth = employee.DateOfBirth,
                    DateOfEmployment = employee.DateOfEmployment,
                    Position = employee.Position,
                    Id = employee.Id
                });
            }
        }

        public  IPerson Find<T>(string passNumber) where T : IPerson
        {
            List<Client> cl = new List<Client>();
            List<Employee> emp = new List<Employee>();

            var findNameEmp =
           from employee in employees
           where employee.PassNumber == passNumber
           select employee;
            if (findNameEmp.Count() == 0)
            {
                var findNameCl =
                    from client in clients
                    where client.PassNumber == passNumber
                    select client;
                foreach (var item in findNameCl)
                {
                    cl.Add(new Client
                    {
                        Name = item.Name,
                        DateOfBirth = item.DateOfBirth,
                        PassNumber = item.PassNumber,
                        Id = item.Id
                    });
                }
            }
            else
            {
                foreach (var item in findNameEmp)
                {
                    emp.Add(new Employee
                    {
                        Name = item.Name,
                        DateOfBirth = item.DateOfBirth,
                        PassNumber = item.PassNumber,
                        Id = item.Id,
                        DateOfEmployment = item.DateOfEmployment,
                        Position = item.Position
                    });
                }
            }
            if (cl.Count != 0) { return cl[0]; } else { return emp[0]; }
        }

        public  Employee FindEmployee(string passNumber)
        {
            var emp = employees;

            return
           (from employee in emp
            where employee.PassNumber == passNumber
            select new Employee
            {
                Name = employee.Name,
                PassNumber = employee.PassNumber,
                DateOfBirth = employee.DateOfBirth,
                Id = employee.Id
            }).FirstOrDefault();
        }

        public  Client FindClient(string passNumber)
        {
            var cl = clients;

            return
            (from client in cl
             where client.PassNumber == passNumber
             select new Client
             {
                 Name = client.Name,
                 PassNumber = client.PassNumber,
                 DateOfBirth = client.DateOfBirth,
                 Id = client.Id
             }).FirstOrDefault();
        }

        public  IPerson Find<T>(T person) where T : IPerson
        {

            if (person is Employee)
            {
                //var pers = person as Employee;
                return
               (from employee in employees
                where employee.PassNumber == person.PassNumber
                select new Employee
                {
                    Name = employee.Name,
                    PassNumber = employee.PassNumber,
                    DateOfBirth = employee.DateOfBirth,
                    Id = employee.Id
                }).FirstOrDefault();
            }
            else
            {
                //var pers1 = person as Client;
                return
              (from client in clients
               where client.PassNumber == person.PassNumber
               select new Client
               {
                   Name = client.Name,
                   PassNumber = client.PassNumber,
                   DateOfBirth = client.DateOfBirth,
                   Id = client.Id
               }).FirstOrDefault();

            }

        }

        public  void MoneyTransfer(int sum, Account accountFrom, Account accountTo, ExchangeDelegate exchangeDelegate) 
        {
            decimal result = exchangeDelegate(sum, accountFrom.CurrencyType, accountTo.CurrencyType);
        }



    }



}


using System;
using System.Collections.Generic;

namespace ProjImposto

{
    class Contract
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public double TotalValue { get; set; }
    }

    class Program
    {
        static List<Tuple<DateTime, double>> CalculateInstallments(Contract contract, int months, double interestRate, double paymentFee)
        {
            List<Tuple<DateTime, double>> installments = new List<Tuple<DateTime, double>>();
            DateTime installmentDate = contract.Date.AddMonths(1);
            double installmentValue = contract.TotalValue / months;

            for (int i = 0; i < months; i++)
            {
                double interestAmount = installmentValue * interestRate / 100;
                double totalAmount = installmentValue + interestAmount;
                double paymentFeeAmount = totalAmount * paymentFee / 100;
                totalAmount += paymentFeeAmount;
                installments.Add(new Tuple<DateTime, double>(installmentDate, totalAmount));

                installmentDate = installmentDate.AddMonths(1);
            }

            return installments;
        }

        static void Main(string[] args)
        {
            // Leitura dos dados do contrato
            Console.Write("Enter contract number: ");
            int contractNumber = int.Parse(Console.ReadLine());

            Console.Write("Date (dd/MM/yyyy): ");
            DateTime contractDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.Write("Contract value: ");
            double contractValue = double.Parse(Console.ReadLine());

            // Leitura do número de parcelas
            Console.Write("Enter number of installments: ");
            int installmentsCount = int.Parse(Console.ReadLine());

            // Criação do objeto contrato
            Contract contract = new Contract
            {
                Number = contractNumber,
                Date = contractDate,
                TotalValue = contractValue
            };

            // Cálculo e exibição das parcelas
            List<Tuple<DateTime, double>> installments = CalculateInstallments(contract, installmentsCount, 1, 2);

            Console.WriteLine("Installments:");
            foreach (Tuple<DateTime, double> installment in installments)
            {
                Console.WriteLine($"{installment.Item1:dd/MM/yyyy} - {installment.Item2:F2}");
            }
        }
    }
}
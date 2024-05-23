using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fortitude.Model.LoansModels;

namespace Fortitude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        #region interest rate
        private double GetMonthlyInterestRate(double loanAmount)
        {
             //8 % per annum fixed rate for loan amounts of RM5, 000 to RM20, 000
             //7 % per annum fixed rate for loan amounts of RM20, 001 to RM50, 000
             //6.5 % per annum fixed rate for loan amounts of RM50, 001 to RM100, 000

            if (loanAmount >= 5000 && loanAmount <= 20000)
                return 0.08 / 12;
            else if (loanAmount >= 20001 && loanAmount <= 50000)
                return 0.07 / 12;
            else if (loanAmount >= 50000 && loanAmount <= 100000)
                return 0.065 / 12;
            else
                return 0.065 / 12;
        }
        #endregion
         
        #region formula
        private double CalculateEMI(double principal, double monthlyInterestRate, int loanPeriodMonths)
        {
            // EMI formula: P x r x (1 + r)n / ((1 + r)n - 1)
            double emi = (principal * monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, loanPeriodMonths)) /
                         (Math.Pow(1 + monthlyInterestRate, loanPeriodMonths) - 1);
            return emi;
        }
        #endregion

        #region task 1 Basic
        [HttpPost("CalculateEMI")]
        public IActionResult CalculateEMI(LoanApply loanDetails)
        {
            try
            {
                if (loanDetails.Amount != 0 && loanDetails.Years != 0)
                {
                    double monthlyRate = GetMonthlyInterestRate(loanDetails.Amount);
                    int numOfPayment = loanDetails.Years * 12;

                    double emi = CalculateEMI(loanDetails.Amount, monthlyRate, numOfPayment);

                    return Ok(new { EMI = "RM " + emi.ToString("F2") });
                }
                else
                {
                    return BadRequest("Invalid value");
                }
            }
            catch(Exception ex)
            {
                var message = "";
                if(loanDetails.Amount < 5000)
                {
                    message = "Minimum Amount is RM5000";
                }
                else if(loanDetails.Years <= 0)
                {
                    message = "Minimum Year is 1";
                }
                else
                {
                    message = ex.Message;
                }
                return BadRequest(message);
            }
        }
        #endregion

        #region task 1 Advance-1
        [HttpGet("CalculateLoanPeriod")]
        public IActionResult CalculateLoanPeriod(double LoanAmount, double targetMonthlyInstallment)
        {
            try
            {
                if (LoanAmount != 0 && targetMonthlyInstallment != 0)
                {
                    double interestRatePerYear = GetMonthlyInterestRate(LoanAmount);
                    double monthlyInterestRate = interestRatePerYear / 12;

                    int loanPeriodInYears = CalculateLoanPeriod(LoanAmount, monthlyInterestRate, targetMonthlyInstallment);

                    return Ok("Estimated Loan Period " + loanPeriodInYears + " Years");
                }
                else
                {
                    return BadRequest("Invalid value");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        private int CalculateLoanPeriod(double LoanAmount, double monthlyInterestRate, double targetMonthlyInstallment)
        {
            int loanTenureInYears = 1;
            while (true)
            {
                int numberOfPayments = loanTenureInYears * 12;
                double emi = CalculateEMI(LoanAmount, monthlyInterestRate, numberOfPayments);
                if (emi <= targetMonthlyInstallment)
                {
                    return loanTenureInYears;
                }
                loanTenureInYears++;
            }
        }
        #endregion

        #region task 1 Advance-2
        [HttpGet("CalculateLoanAmount")]
        public IActionResult CalculateLoanAmount(double monthlyInstallment, int loanTenureInYears, double interestRatePerAnnum)
        {
            try
            {
                if(monthlyInstallment != 0 && loanTenureInYears != 0 && interestRatePerAnnum != 0)
                {
                    double monthlyInterestRate = interestRatePerAnnum / (12 * 100);
                    int numberOfPayments = loanTenureInYears * 12;

                    double loanAmount = monthlyInstallment * (((Math.Pow(1 + monthlyInterestRate, numberOfPayments) - 1) /
                        (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, numberOfPayments))));

                    return Ok("Estimated Loan Amount RM " + loanAmount.ToString("F0"));
                }
                else
                {
                    return BadRequest("Invalid value");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion
    }
}

namespace Lab9;
public class BasicMath{
 public double Add(double a,double b)=>a+b;
 public double Subtract(double a,double b)=>a-b;
 public double Multiply(double a,double b)=>a*b;
 public double Divide(double a,double b)=>b==0?throw new DivideByZeroException():a/b;
}
public class BankAccount{
 public decimal Balance {get;private set;}
 public BankAccount(decimal opening){if(opening<0)throw new ArgumentOutOfRangeException();Balance=opening;}
 public void Credit(decimal amount){if(amount<=0)throw new ArgumentOutOfRangeException();Balance+=amount;}
 public void Debit(decimal amount){if(amount<=0)throw new ArgumentOutOfRangeException();if(amount>Balance)throw new InvalidOperationException("Insufficient funds");Balance-=amount;}
 public void Withdraw(decimal amount)=>Debit(amount);
}

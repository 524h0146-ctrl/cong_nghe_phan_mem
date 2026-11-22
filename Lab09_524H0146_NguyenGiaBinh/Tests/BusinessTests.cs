using Xunit;using System.Globalization;using OpenQA.Selenium;using OpenQA.Selenium.Chrome;
namespace Lab9;
public class Cases {
 static IEnumerable<string[]> Csv(string file)=>File.ReadLines(Path.Combine(AppContext.BaseDirectory,"Data",file)).Skip(1).Select(line=>line.Split(','));
 public static IEnumerable<object[]> Numbers(){foreach(var op in new[]{"Add","Subtract","Multiply","Divide"})foreach(var r in Csv(op+".csv"))yield return new object[]{op,double.Parse(r[0],CultureInfo.InvariantCulture),double.Parse(r[1],CultureInfo.InvariantCulture),double.Parse(r[2],CultureInfo.InvariantCulture)};}
 [Theory,MemberData(nameof(Numbers))]public void Arithmetic(string op,double a,double b,double expected){var m=new BasicMath();Assert.Equal(expected,op switch{"Add"=>m.Add(a,b),"Subtract"=>m.Subtract(a,b),"Multiply"=>m.Multiply(a,b),_=>m.Divide(a,b)},8);}
 [Fact]public void ZeroDivisor(){Assert.Throws<DivideByZeroException>(()=>new BasicMath().Divide(1,0));}
 public static IEnumerable<object[]> Accounts()=>Csv("BankAccountTestData.csv").Select(r=>new object[]{int.Parse(r[0]),int.Parse(r[1]),r[2]});
 [Theory,MemberData(nameof(Accounts))]public void Debit(int opening,int amount,string expected){var a=new BankAccount(opening);if(expected=="error"){Assert.Throws<InvalidOperationException>(()=>a.Debit(amount));Assert.Equal((decimal)opening,a.Balance);}else{a.Debit(amount);Assert.Equal(decimal.Parse(expected),a.Balance);}}
 [Fact]public void CreditAndWithdraw(){var a=new BankAccount(100);a.Credit(25);a.Withdraw(120);Assert.Equal(5m,a.Balance);}
 public static IEnumerable<object[]> Logins()=>Csv("LoginTestData.csv").Select(r=>new object[]{r[0],r[1],r[2]});
 [Theory,MemberData(nameof(Logins))]public void BrowserLogin(string user,string pass,string expected){var options=new ChromeOptions();options.AddArgument("--headless=new");using var browser=new ChromeDriver(options);
 browser.Navigate().GoToUrl(new Uri(Path.Combine(AppContext.BaseDirectory,"Data","login.html")).AbsoluteUri);
 browser.FindElement(By.Id("username")).SendKeys(user);browser.FindElement(By.Id("password")).SendKeys(pass);browser.FindElement(By.Id("loginButton")).Click();Assert.Equal(expected,browser.FindElement(By.Id("result")).Text);}
}

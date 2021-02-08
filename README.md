# Shopping List

Ett console applikation där en användare kan lägga produkter i en lista(stack). 
Här använde jag mig av design mönster och ett SOLID princip.

## Abstract Factory Pattern

Jag använde mig av abstract factory pattern för att sätta betalningstyp. Har en abstrakt klass PaymentFactory, där två konkreta klasser (DebitFactory & CreditFactory) ärver ifrån.

```cs
public abstract class PaymentFactory
{
  public abstract IPayment GetPayment(string paymentType);

  public static PaymentFactory CreatePaymentFactory(string factoryType)
  {
     if (factoryType.ToLower() == "credit")
           return new CreditFactory();

     return new DebitFactory();
  }
}
```
```cs
public class DebitFactory : PaymentFactory
{
   public override IPayment GetPayment(string paymentType)
   {
     	if (paymentType.ToLower() == "mastercard")
        {
            return new MasterCard();
        }
        else if (paymentType.ToLower() == "maestro")
        {
            return new Maestro();
        }

        return null;
   }
}
```
## Command Pattern

Jag använde mig av Command pattern för att lägga till, undo:a, redo:a och rensa en stack. User är den klasses som anropar och Order klassen är mottageren.

```cs
public interface ICommand
{
   void Execute(Stack<Product> order, Product newProduct);
}
```
```cs
public void Execute(Stack<Product> products, Product newProduct)
{
   products.Push(newProduct);
}
```

## Open–Closed Principle

Jag använde mig av open-closed principle för filtrering av listan med produkter, som använderan har lagt till. Har en klass ProductFilter som ärver från interface:t IFilter.
Filter metoden i ProductFilter används helt enkelt för filtrering, baserat på vilken lista man vill filtrera och sökspecifikation. 
Har två klasser med olika sökspecifikation (Category & Color) som ärver från ISpecification.

```cs
public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
{
   foreach (var item in items)
   {
       if (spec.DoesMatch(item))
       {
           yield return item;
       }
   }
}
```  
```cs
public class ColorSpecification : ISpecification<Product>
{
   private string color;

   public ColorSpecification(string color)
   {
      this.color = color;
   }
   public bool DoesMatch(Product product)
   {
      return product.Color == color;
   }
}
``` 
Principen går ut på att att klasser är öppna för en förlängning men stängd för modifiering.
Så ville jag implementera en ny sökspecifikation så skapar jag en ny klass som i sin tur ärver från interface:t.

## Events

Jag valde att använda ett event i User klassen, specifikt SetCommandOption metoden, där alla som väljer att "lyssna" på eventet får ett meddelande baserat på alternativ man har valt att utföra.

```cs
public event EventHandler<ChosenCommandEventArgs> ChosenCommand;
```
```cs
string message = null;
switch (option)
{
   case 1:
      message = "Added product to list";
      break;
   case 2:
      message = "You choose to redo last added product";
      break;
   case 3:
      message = "You choose to undo last added product";
      break;
   case 4:
      message = "Why did you do that!?";
      break;
}
ChosenCommand?.Invoke(this, new ChosenCommandEventArgs { message = message });
```
using System;
using System.Diagnostics; 

namespace FloatAddition
{
    class Program
    {
        public static int finalSign=0;  
        static void Main(string[] args)
        {
            int signFloat1=0,signFloat2=0;
            double finalSum;
            string firstString, secondString;
                                
            //Input Value 1
            Console.Write("Enter first float: ");
            firstString = Console.ReadLine();
            if(firstString[0]=='-')
            {
                firstString = firstString.Substring(1);
                signFloat1 = 1;
            }

            // Input Value 2
            Console.Write("Enter second float: ");
            secondString = Console.ReadLine();
            if(secondString[0]=='-')
            {
                secondString = secondString.Substring(1);
                signFloat2 = 1;
            }

            // Cleaning of the given Data
            if(firstString.IndexOf('.')<0){
                firstString = firstString + '.' + '0';
            }
            if(secondString.IndexOf('.')<0){
                secondString = secondString + '.' + '0';
            }
            if(firstString.IndexOf('.') == 0){
                firstString = "0" + firstString;
            }
            if(secondString.IndexOf('.') == 0){
                secondString = "0" + secondString;
            }

            // Converting Fractional Part into Binary
            string floatDec1 = FractionToBinary(float.Parse(firstString.Substring(firstString.IndexOf('.'))));
            string floatDec2 = FractionToBinary(float.Parse(secondString.Substring(secondString.IndexOf('.'))));

            // Coverting the Integer Part into Binary
            string floatInt1 = IntToBinary((int)float.Parse(firstString));
            string floatInt2 = IntToBinary((int)float.Parse(secondString));

            // Combining Both Fractional Part and Integer Part
            string finalFloat1 = floatInt1 + "." + floatDec1;
            string finalFloat2 = floatInt2 + "." + floatDec2;

            if(signFloat1 == signFloat2){
                if(finalFloat1.IndexOf('.') == finalFloat2.IndexOf('.')){
                    finalSum = AddBothPosOrNeg(finalFloat1, finalFloat2);
                }
                else if(finalFloat1.IndexOf('.') < finalFloat2.IndexOf('.')){
                    while(finalFloat1.IndexOf('.') != finalFloat2.IndexOf('.')){
                        finalFloat1 = '0'+finalFloat1;
                    }
                    finalSum = AddBothPosOrNeg(finalFloat1, finalFloat2);
                }
                else{
                    while(finalFloat1.IndexOf('.') != finalFloat2.IndexOf('.')){
                        finalFloat2 = '0'+finalFloat2;
                    }
                    finalSum = AddBothPosOrNeg(finalFloat1, finalFloat2);
                }
                if(signFloat1==0){
                    Console.WriteLine("The Sum is: "+finalSum);
                }
                else{
                    finalSum = (-1) * finalSum;
                    Console.WriteLine("The Sum is: "+finalSum);
                }
            }
            else if(signFloat1 != signFloat2){
                if(finalFloat1.IndexOf('.') == finalFloat2.IndexOf('.')){
                    if(signFloat1==1 && signFloat2==0){
                        finalFloat1 = TwosComplement(finalFloat1);
                    }
                    else if(signFloat1==0 && signFloat2==1){
                        finalFloat2 = TwosComplement(finalFloat2);
                    }
                    finalSum = AddOnlyPosNeg(finalFloat1, finalFloat2);
                }
                else if(finalFloat1.IndexOf('.') < finalFloat2.IndexOf('.')){
                    while(finalFloat1.IndexOf('.') != finalFloat2.IndexOf('.')){
                        finalFloat1 = '0'+finalFloat1;
                    }
                    if(signFloat1==1 && signFloat2==0){
                        finalFloat1 = TwosComplement(finalFloat1);
                    }
                    else if(signFloat1==0 && signFloat2==1){
                        finalFloat2 = TwosComplement(finalFloat2);
                    }
                    finalSum = AddOnlyPosNeg(finalFloat1, finalFloat2);
                }
                else{
                    while(finalFloat1.IndexOf('.') != finalFloat2.IndexOf('.')){
                        finalFloat2 = '0'+finalFloat2;
                    }
                    if(signFloat1==1 && signFloat2==0){
                        finalFloat1 = TwosComplement(finalFloat1);
                    }
                    else if(signFloat1==0 && signFloat2==1){
                        finalFloat2 = TwosComplement(finalFloat2);
                    }
                    finalSum = AddOnlyPosNeg(finalFloat1, finalFloat2);
                }
                if(finalSign==1){
                    finalSum = (-1) * finalSum;
                    Console.WriteLine("Sum is: "+ finalSum);
                }
                else{
                    Console.WriteLine("Sum is: "+finalSum);
                }
            }

            if(Debugger.IsAttached)
            {
                Console.WriteLine("Press Any Key to Quit...");
                Console.ReadKey();
            }
        }

        // add(string s1, string s2) takes two float valued binary strings and add them. This is fired when both the values have same sign
        public static double AddBothPosOrNeg(string s1, string s2){
            int Remainder=0, i, powVal=0;
            string sum = string.Empty;
            double result=0;
            for(i=s1.Length-1;i>=0;i--){
                if(s1[i] == '.'){
                    sum = sum  + '.';
                }
                else{
                    sum = sum+(((int)s1[i])-'0' + ((int)s2[i])-'0' + Remainder)%2;
                    Remainder = (((int)s1[i])-'0' + ((int)s2[i])-'0' + Remainder)/2;
                }
            }
            if(Remainder!=0){
                sum = sum+(Remainder%2);
            }
            i=0;
            while(sum[i]!='.'){
                result = result + (((int)sum[i])-'0')*Power(i-8);
                i++;
            }
            for(int j=sum.IndexOf('.')+1;j<sum.Length;j++){
                result = result + (((int)sum[j])-'0')*(int)(Power(powVal));
                powVal++; 
            }
            return result;
        }

        public static double AddOnlyPosNeg(string s1, string s2){
            int Remainder=0, i, powVal=0;
            string sum = string.Empty;
            double result=0;
            for(i=s1.Length-1;i>=0;i--){
                if(s1[i] == '.'){
                    sum = sum  + '.';
                }
                else{
                    sum = sum+(((int)s1[i])-'0' + ((int)s2[i])-'0' + Remainder)%2;
                    Remainder = (((int)s1[i])-'0' + ((int)s2[i])-'0' + Remainder)/2;
                }
            }
            if(Remainder==0){
                char[] temp = sum.ToCharArray();
                Array.Reverse(temp);
                sum = TwosComplement(new string(temp));
                temp = sum.ToCharArray();
                Array.Reverse(temp);
                sum = new string(temp);
                finalSign=1;
            }
            i=0;
            while(sum[i]!='.'){
                result = result + (((int)sum[i])-'0')*Power(i-8);
                i++;
            }
            for(int j=sum.IndexOf('.')+1;j<sum.Length;j++){
                result = result + (((int)sum[j])-'0')*(int)(Power(powVal));
                powVal++; 
            }
            return result;
        }

        // returns the 2^(power) and 2^(-power) value
        public static double Power(int temp){
            double power=1;
            int powerValue, flag=0;
            flag = temp>0 ? 1 : -1; 
            powerValue = flag * temp;
            while(powerValue>0){
                power = power*2;
                powerValue--;
            }
            return temp>0 ? power : (double)(1/power);
        }

        // Converts the Integer part into Binary
        public static string IntToBinary(int temp){
            string tempBin = string.Empty;
            while(temp>0){
                tempBin = tempBin+temp%2;
                temp = temp/2;
            } 
            char[] tempC = tempBin.ToCharArray();
            Array.Reverse(tempC);
            return new string(tempC);
        }

        public static string FractionToBinary(float temp){
            string tempBin = string.Empty;
            for(int i=0;i<8;i++){
                tempBin = tempBin + (int)(temp*2);
                temp = temp*2 - (int)(temp*2);
            }
            return tempBin;
        }

        // Returns Two's Complement of given binary valued string
       public static string TwosComplement(string number){
            int Remainder = 1;
            string temp1 = string.Empty;
            string temp2 = string.Empty;
            for(int i=0;i<number.Length;i++){
                if(number[i]=='0'){
                    temp1 = temp1 + '1';
                }
                else if(number[i]=='1'){
                    temp1 = temp1 + '0';
                }
                else if(number[i]=='.'){
                    temp1 = temp1 + '.';
                }
            }
            for(int i=temp1.Length-1;i>=0;i--){
                if(temp1[i] == '.'){
                    temp2 = temp2  + '.';
                }
                else{
                    temp2 = temp2+(((int)temp1[i])-'0' + Remainder)%2;
                    Remainder = (((int)temp1[i])-'0' + Remainder)/2;
                }
            }
            if(Remainder!=0){
                temp2 = temp2+(Remainder%2);
            }
            char[] temp3 = temp2.ToCharArray();
            Array.Reverse(temp3);
            return new string(temp3);
        }
    }
}
/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package aplicacionjava8;

/**
 *
 * @author Admin
 */
public class AplicacionJava8 {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        String originalString = "This is the original String";
        
        System.out.println(originalString.substring(0, originalString.length()));
        System.out.println(originalString.substring(5, 20));
        System.out.println(originalString.substring(12));
        
        String myString = " This is a String that contains whitespace.            ";
        System.out.println(myString);
        System.out.println(myString.trim());
        
        String str = "Break down into chars";
        System.out.println(str);
        
        for (char chr:str.toCharArray()){
            System.out.println(chr);
        }
    }
    
}

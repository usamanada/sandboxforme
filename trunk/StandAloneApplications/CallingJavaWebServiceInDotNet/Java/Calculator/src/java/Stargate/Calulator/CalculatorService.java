/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Stargate.Calulator;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebService;

/**
 *
 * @author Administrator
 */
@WebService()
public class CalculatorService
{
    @WebMethod
    public int Add(@WebParam(name="a") int a,@WebParam(name="b") int b)
    {
        return a + b;
    }
}

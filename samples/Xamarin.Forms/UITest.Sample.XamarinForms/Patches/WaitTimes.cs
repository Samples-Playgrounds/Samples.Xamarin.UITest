using System;

namespace HolisticWare.XamarinUITest
{
	public class WaitTimes : Xamarin.UITest.Utils.IWaitTimes
{
    public TimeSpan GestureWaitTimeout
    {
        get { return TimeSpan.FromMinutes(1); }
    }
    public TimeSpan WaitForTimeout
    {
        get { return TimeSpan.FromMinutes(1); }
    }
}}


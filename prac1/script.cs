using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auto_bitcoin
{
    public class script
    {
        public static string checkSite() {
            return @"
            var titlex = document.getElementsByTagName('title')[0].innerHTML;
            if(titlex.indexOf('Dashboard') > -1){
                bound.checkLogin('true');
                bound.checkReady('false');
                document.location.href='/en/trade/BTC_USDT?layout=basic';
            } else if(titlex.indexOf('Binance Spot') > -1){
                bound.checkLogin('true');
                bound.checkReady('true');
            } else {
                bound.checkLogin('false');
                bound.checkReady('false');
            }
            ";
        }

        public static string go() {
            return @"
                function goTrade(max, min, amount){
                    var value = document.getElementsByClassName('showPrice')[0].innerHTML;
                    value = value.replaceAll(',','');
                    if(max < value){ //팔기
                        document.getElementById('FormRow-SELL-quantity').value = amount;
                        document.getElementById('FormRow-SELL-price').value = max;
                        document.getElementById('orderformSellBtn').click();
                    }
                    if(min > value){
                        document.getElementById('FormRow-BUY-quantity').value = amount;
                        document.getElementById('FormRow-BUY-price').value = min;
                        document.getElementById('orderformBuyBtn').click();
                    }
                }

console.log();

";
        
        }

    }
}

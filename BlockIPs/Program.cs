using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockIPs
{
    class Program
    {
        // Maying a 'monitoring' phase where it looks for the IP's that are on the block list and notes their image/exepath

        // Add references to these Dll's 
        // C:\Windows\System32\hnetcfg.dll
        // C:\Windows\System32\FirewallAPI.dll
        
        static void Main(string[] args)
        {
            // ToDo: Print Status of FireWall
            // ToDo: Confirm by listing all the Firewall rules

            INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FWRule"));

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));



            // Application based - does not seem to be working
            /*
            firewallRule.Name = "Block_App_Rule";
            //firewallPolicy.Rules.Remove(firewallRule.Name);
            firewallRule.ApplicationName = @"C:\Users\seanp\Desktop\putty.exe";
            firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            firewallRule.Description = "Block App";
            firewallRule.Enabled = true;
            firewallRule.InterfaceTypes = "All";
            firewallRule.Profiles = firewallPolicy.CurrentProfileTypes;
            firewallPolicy.Rules.Add(firewallRule);
            //firewallPolicy.Rules.Remove(firewallRule.Name);
            */
            
            var ipBlockList = new List<string>();
            ipBlockList.Add("8.8.8.8");
            ipBlockList.Add("206.55.190.63");
            string ipBlockListString = String.Join(",",ipBlockList);

            firewallRule.Name = "Block_IP_Rule";
            firewallRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            //firewallRule.LocalPorts = "4000";
            firewallRule.RemoteAddresses = ipBlockListString;       // https://docs.microsoft.com/en-us/windows/win32/api/netfw/nf-netfw-inetfwrule-get_remoteaddresses
            firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
            firewallRule.Description = "Block outbound traffic to " + firewallRule.RemoteAddresses + " over TCP"; //  " port " + firewallRule.LocalPorts;
            firewallRule.Enabled = true;
            // firewallRule.Grouping = "@firewallapi.dll,-23255";  // https://docs.microsoft.com/en-us/windows/win32/api/netfw/nf-netfw-inetfwrule-put_grouping
            firewallRule.Profiles = firewallPolicy.CurrentProfileTypes;
            firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            firewallRule.InterfaceTypes = "All";
            firewallPolicy.Rules.Add(firewallRule);
            //firewallPolicy.Rules.Remove(firewallRule.Name);


        }


    }
}

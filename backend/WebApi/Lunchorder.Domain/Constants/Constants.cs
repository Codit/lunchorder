using System;
using System.Collections.Generic;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Domain.Constants
{
    public class ApplicationConstants
    {
        public const string AuthTokenIdentifier = "access_token";
    }

    public static class Badges
    {
        public static readonly Badge HaveFaith = new Badge(Guid.Parse("1736254d-48ee-4d53-9834-300d64814d1e"), "Have Faith", true, "Make a deposit of €100 at once", "have_faith_200x200.png", "have_faith_800x800.png");
        public static readonly Badge BadgeCollector = new Badge(Guid.Parse("75d335ab-18db-440e-ae7d-f3f6615dd9ab"), "Badge Collector", false, "Collect a total of 10 badges", "collector_10_200x200.png", "badge_collector_10_800x800.png");
        public static readonly Badge HighRoller = new Badge(Guid.Parse("e1d4752b-1a85-48ae-bb61-8532e1e58926"), "High Roller", true, "Spend €30 euros in one week", "high_roller_200x200.png", "badge_high_roller_800x800.png");
        public static readonly Badge DeepPockets = new Badge(Guid.Parse("b8f30097-3d08-4ade-8b89-478d6b48e540"), "Deep Pockets", false, "Spend €75 euros in one month", "deep_pockets_200x200.png", "deep_pockets_800x800.png");
        public static readonly Badge Consumer = new Badge(Guid.Parse("f7f54638-755c-4c93-aacb-e2f2bcc91287"), "Consumer", false, "Spend more than €500 euros in total", "consumer_200x200.png", "consumer_800x800.png");
        public static readonly Badge Enjoyer = new Badge(Guid.Parse("6dcb39fd-1d82-4380-9e6a-c71d4fb13669"), "Enjoyer", false, "Spend more than €1500 euros in total", "enjoyer_200x200.png", "enjoyer_800x800.png");
        public static readonly Badge DieHard = new Badge(Guid.Parse("786fcd84-7fe5-4e7a-83fc-95e051934134"), "Die Hard", false, "Spend more than €3000 euros in total", "die_hard_200x200.png", "die_hard_800x800.png");
        public static readonly Badge Bankrupt = new Badge(Guid.Parse("e9c851f6-5c4c-4815-82ba-f844e15a0645"), "Bankrupt", true, "Make an order so your balance reaches exact €0", "bankrupt_200x200.png", "bankrupt_800x800.png");
        public static readonly Badge CloseCall15 = new Badge(Guid.Parse("d7a6e155-cee8-4be7-8216-e2c4fea53b0c"), "Close Call 15", true, "Make an order 15 seconds before the final order time", "close_call_15sec_200x200.png", "close_call_15sec_800x800.png");
        public static readonly Badge CloseCall30 = new Badge(Guid.Parse("63150f8b-aa56-4555-9539-d4311745f1a9"), "Close Call 30", true, "Make an order 30 seconds before the final order time", "close_call_30sec_200x200.png", "close_call_30sec_800x800.png");
        public static readonly Badge CloseCall45 = new Badge(Guid.Parse("9fe7a8f4-fae1-4508-a9ae-c085c6a16863"), "Close Call 45", true, "Make an order 45 seconds before the final order time", "close_call_45sec_200x200.png", "close_call_45sec_800x800.png");
        public static readonly Badge CloseCall60 = new Badge(Guid.Parse("555e21c8-e7f5-4455-af27-9bd11f6eced9"), "Close Call 60", true, "Make an order 60 seconds before the final order time", "close_call_60sec_200x200.png", "close_call_60sec_800x800.png");
        public static readonly Badge DoubleOrder = new Badge(Guid.Parse("09296d9f-c2f8-4325-9282-546ae42822f2"), "Double Order", true, "Make 2 individual orders on the same day", "double_order_200x200.png", "double_order_800x800.png");
        public static readonly Badge FirstOrder = new Badge(Guid.Parse("15a6a9b3-793b-40ea-adf6-bf8e09d74352"), "First Order", false, "Make your first order using this lunch application", "first_order_200x200.png", "first_order_800x800.png");
        public static readonly Badge Healty = new Badge(Guid.Parse("0e094fa6-14ed-4fed-86a5-49f52bebcafb"), "Healthy", true, "Make 3 healthy orders in the same week", "healthy_200x200.png", "healthy_800x800.png");
        public static readonly Badge Mobile = new Badge(Guid.Parse("c2d86e6b-2bb7-4495-9aed-36aad10f5b73"), "Mobile", false, "Make an order using the mobile website version of the lunch application", "mobile_200x200.png", "mobile_800x800.png");

        public static List<Badge> BadgeList => new List<Badge>
        {
            HaveFaith, BadgeCollector, HighRoller, DeepPockets, Consumer, Enjoyer, DieHard, Bankrupt, CloseCall15, CloseCall30, CloseCall45, CloseCall60, DoubleOrder, FirstOrder, Healty, Mobile
        };
    }
}
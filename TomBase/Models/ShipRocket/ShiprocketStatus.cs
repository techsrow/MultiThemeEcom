using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Models.ShipRocket
{
 
    public class ShipmentTrack
    {
        public int id { get; set; }
        public string awb_code { get; set; }
        public int courier_company_id { get; set; }
        public object shipment_id { get; set; }
        public int order_id { get; set; }
        public string pickup_date { get; set; }
        public string delivered_date { get; set; }
        public string weight { get; set; }
        public int packages { get; set; }
        public string current_status { get; set; }
        public string delivered_to { get; set; }
        public string destination { get; set; }
        public string consignee_name { get; set; }
        public string origin { get; set; }
        public object courier_agent_details { get; set; }
    }

    public class ShipmentTrackActivity
    {
        public string date { get; set; }
        public string activity { get; set; }
        public string location { get; set; }
    }

    public class TrackingData
    {
        public int track_status { get; set; }
        public int shipment_status { get; set; }
        public List<ShipmentTrack> shipment_track { get; set; }
        public List<ShipmentTrackActivity> shipment_track_activities { get; set; }
        public string track_url { get; set; }
    }

    public class ShipRocketTracking
    {
        public TrackingData tracking_data { get; set; }
    }

    public class OrderInfoData
    {
        public ShipRocketTracking ShipRocketTracking { get; set; }
        public BasePackageModule2.Models.Order order{ get; set; }
    }

}

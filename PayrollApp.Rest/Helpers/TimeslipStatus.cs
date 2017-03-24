using PayrollApp.Core.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollApp.Rest.Helpers
{
    public class RequestData
    {
        public int JobDuration { get; set; }
        public long CustomerID { get; set; }
        public DateTime WorkStartRsv { get; set; }
        public TimeSpan StartTimeRsv { get; set; }
        public long LabourClassificationID { get; set; }
        public string DayOfWeek { get; set; }
        public List<DayStatus> DayStatusList { get; set; }
        public long OrderTimeslipID { get; set; }
        public DateTime DispatchDate { get; set; }
    }



    public class TimeslipStatus
    {
        public const byte SCRATCHED = 0;
        public const byte TENTATIVE = 1;
        public const byte REQUESTED = 2;
        public const byte SENT = 3;
        public const byte OFF = 4;
        public const byte FINISHED = 5;


        public const int RESERVED = -2;
        public const int DISPATCHED = -1;
        public const int POSTED = 0;
        public const int PAYROLL_POSTED = 1;
        public const int INVOICE_POSTED = 2;
        public const int QUEUED = 3;
        public int BillState = RESERVED;
        public bool IsOneDay = false;


        public enum DrawStyle
        {
            Order,		// status on top and roll on bottom
            Summary,	// status on left and roll on right (1 line)
            Timeslip	// big labels, vertically aligned with Timeslip rows
        }

        public TimeslipStatus()
        {

        }

        public List<DayStatus> DayStatusList = new List<DayStatus>();
        private DateTime _startOfWeek = Utility.NullDateValue;
        private DateTime _endOfWeek = Utility.NullDateValue;
        private DateTime _confirmedThrough = Utility.NullDateValue;
        private DateTime _rollOverDate = Utility.NullDateValue;

        public bool IsToRollTomorrow = false;


        public DateTime EndOfWeek
        {
            get { return _endOfWeek; }
        }

        public DateTime StartOfWeek
        {
            get { return _startOfWeek; }
        }

        private byte[] _customerWorkWeek = DEFAULT_CUSTOMER_WORKWEEK;

        public static byte[] DEFAULT_CUSTOMER_WORKWEEK
        {
            get
            {
                switch (DayOfWeek.ToUpper())
                {
                    case "SUNDAY": return new byte[7] { TENTATIVE, TENTATIVE, TENTATIVE, TENTATIVE, TENTATIVE, OFF, OFF };
                    case "MONDAY": return new byte[7] { TENTATIVE, TENTATIVE, TENTATIVE, TENTATIVE, OFF, OFF, TENTATIVE };
                    case "TUESDAY": return new byte[7] { TENTATIVE, TENTATIVE, TENTATIVE, OFF, OFF, TENTATIVE, TENTATIVE };
                    case "WEDNESDAY": return new byte[7] { TENTATIVE, TENTATIVE, OFF, OFF, TENTATIVE, TENTATIVE, TENTATIVE };
                    case "THURSDAY": return new byte[7] { TENTATIVE, OFF, OFF, TENTATIVE, TENTATIVE, TENTATIVE, TENTATIVE };
                    case "FRIDAY": return new byte[7] { OFF, OFF, TENTATIVE, TENTATIVE, TENTATIVE, TENTATIVE, TENTATIVE };
                    case "SATURDAY": return new byte[7] { OFF, TENTATIVE, TENTATIVE, TENTATIVE, TENTATIVE, TENTATIVE, OFF };
                    default: goto case "SUNDAY";
                }
            }
        }

        private byte[] _status = new byte[7] { REQUESTED, REQUESTED, REQUESTED, REQUESTED, REQUESTED, REQUESTED, REQUESTED };
        private byte[] _statusToRollTo = new byte[7] { REQUESTED, REQUESTED, REQUESTED, REQUESTED, REQUESTED, REQUESTED, REQUESTED };
        private string[] _statusName = new string[6] { "SCRATCHED", "TENTATIVE", "REQUESTED", "SENT", "OFF", "FINISHED" };
        public static string DayOfWeek { get; set; }
        public string StartingDay { get; set; }
        private DrawStyle _drawStyle = DrawStyle.Summary;

        public TimeslipStatus(String composite, int billState, byte[] customerWorkWeek, string dayOfWeek, DrawStyle drawStyle)
        {
            BillState = billState;
            _customerWorkWeek = customerWorkWeek;
            DayOfWeek = dayOfWeek;
            StartingDay = dayOfWeek;
            _drawStyle = drawStyle;

            String stat = Utility.GetElement(composite, "Stat");
            String roll = Utility.GetElement(composite, "Roll");
            String week = Utility.GetElement(composite, "Week");
            IsOneDay = Utility.ToBoolean(Utility.GetElement(composite, "IsOneDay"));

            _startOfWeek = Utility.ToStartOfDay(Utility.ToDateQB(week.Substring(0, 10)));
            _endOfWeek = Utility.ToEndOfDay(Utility.ToDateQB(week.Substring(11)));

            _confirmedThrough = Utility.ToEndOfDay(Utility.ToDateQB(stat.Substring(7)));
            _rollOverDate = Utility.ToDateQB(roll.Substring(7));

            for (int i = 0; i < 7; i++)
            {
                switch (stat[i])
                {
                    case '0': _status[i] = SCRATCHED; break;
                    case '1': _status[i] = TENTATIVE; break;
                    case '2': _status[i] = REQUESTED; break;
                    case '3': _status[i] = SENT; break;
                    case '4': _status[i] = OFF; break;
                    case '5': _status[i] = FINISHED; break;
                    default: goto case '0';
                }
            }
            for (int i = 0; i < 7; i++)
            {
                switch (roll[i])
                {
                    case '0': _statusToRollTo[i] = SCRATCHED; break;
                    case '1': _statusToRollTo[i] = TENTATIVE; break;
                    case '2': _statusToRollTo[i] = REQUESTED; break;
                    case '3': _statusToRollTo[i] = SENT; break;
                    case '4': _statusToRollTo[i] = OFF; break;
                    case '5': _statusToRollTo[i] = FINISHED; break;
                    default: goto case '0';
                }
            }

            if (drawStyle == DrawStyle.Order)
                DayStatusList = Draw();
            else
                if (drawStyle == DrawStyle.Timeslip)
                    DayStatusList = DrawTimeslip();
        }

        public static string SetDeafultDayOfWeek(string dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
            return dayOfWeek;
        }

        public static String GetDefaultComposite(String activityName, bool isOneDay, DateTime firstWorkDay, byte[] customerStatusWeek, string dayOfWeek)
        {
            DayOfWeek = dayOfWeek;

            if ((customerStatusWeek == null) || (customerStatusWeek.Length != 7))
                customerStatusWeek = DEFAULT_CUSTOMER_WORKWEEK;

            String ret = "<Stat>";

            DateTime startOfWeek = Utility.StartOfWeek(firstWorkDay);
            DateTime endOfWeek = Utility.EndOfWeek(firstWorkDay);

            int indexOfFirstWorkDay = ((System.TimeSpan)(Utility.ToStartOfDay(firstWorkDay).Subtract(startOfWeek))).Days;

            bool isPeicework = isOneDay && activityName.ToUpper().StartsWith("LOAD / UNLOAD");

            int daysInWeek = ((TimeSpan)Utility.ToStartOfDay(endOfWeek).Subtract(startOfWeek)).Days + 1;

            for (int i = 0; i < 7; i++)
            {
                if (i < indexOfFirstWorkDay)
                    ret = String.Concat(ret, Convert.ToChar(SCRATCHED + (byte)'0'));
                else if (i == indexOfFirstWorkDay)
                    ret = String.Concat(ret, Convert.ToChar(REQUESTED + (byte)'0'));
                else
                {
                    if ((i >= daysInWeek) || (isOneDay && (i > (indexOfFirstWorkDay + 2))))
                        ret = String.Concat(ret, Convert.ToChar(SCRATCHED + (byte)'0'));
                    else
                    {
                        if (isPeicework)
                            ret = String.Concat(ret, Convert.ToChar(FINISHED + (byte)'0'));
                        else
                            ret = String.Concat(ret, Convert.ToChar(customerStatusWeek[i] + (byte)'0'));
                    }
                }
            }

            ret = ret + Utility.ToDateQB(firstWorkDay) + "</Stat>";

            ret = ret + "<Roll>";
            foreach (byte day in customerStatusWeek)
                ret = ret + Convert.ToChar(day + (byte)'0');
            ret = ret
                + Utility.ToDateQB(Utility.NullDateValue)
                + "</Roll>";

            ret = ret + "<Week>" + Utility.ToDateQB(startOfWeek) + "^" + Utility.ToDateQB(endOfWeek) + "</Week>";

            ret = String.Concat(ret, "<IsOneDay>", isOneDay ? "true" : "false", "</IsOneDay>");

            return ret;
        }

        public static DateTime GetDefaultRolloverDate(bool isOneDay, DateTime firstWorkDay, DateTime endOfWeek)
        {
            if (isOneDay)
            {
                DateTime lastDayOfWeek = Utility.ToStartOfDay(endOfWeek);
                DateTime d = Utility.ToStartOfDay(firstWorkDay).AddDays(1.0); // 2nd Day
                if (d <= lastDayOfWeek)
                    d = d.AddDays(1.0); // 3rd Day
                if (d <= lastDayOfWeek)
                    d = d.AddDays(1.0); // latest rollover day
                return d;
            }
            else // weekly timeslip
                return endOfWeek.AddDays(1.0);
        }

        public DateTime RollOverDate
        {
            get { return _rollOverDate; }
            set
            {
                if (value == _rollOverDate)
                    return; // no change

                if ((value == Utility.NullDateValue) ||
                    ((value != Utility.NullDateValue) &&
                    ((value >= Utility.ToStartOfDay(_endOfWeek.AddDays(1.0)))
                    || (IsOneDay && (value >= FirstDayInTimeslip.AddDays(3.0)))) //_startOfWeek) && (value <= _endOfWeek)))
                    ))
                    _rollOverDate = Utility.ToStartOfDay(value);
                else
                    _rollOverDate = Utility.NullDateValue;
            }
        }

        public bool IsToBeRolledOver
        {
            get { return RollOverDate != Utility.NullDateValue; }
        }

        public bool IsRollInCurrentWeek
        {
            get { return IsToBeRolledOver && IsDateInWeek(RollOverDate); }
        }

        public bool IsDateInWeek(DateTime date)
        {
            int s = getIndexOfDate(date, false);
            return (s >= 0) && (s < DaysInWeek);
        }

        public int getIndexOfDate(DateTime date, bool isRoll)
        {
            if (isRoll)
                return ((System.TimeSpan)(Utility.ToStartOfDay(date).Subtract(StartOfRollWeek))).Days;
            else
                return ((System.TimeSpan)(Utility.ToStartOfDay(date).Subtract(_startOfWeek))).Days;
        }

        public int DaysInWeek
        {
            get { return ((TimeSpan)Utility.ToStartOfDay(_endOfWeek).Subtract(Utility.ToStartOfDay(_startOfWeek))).Days + 1; }
        }

        public DateTime FirstDayInTimeslip
        {
            get
            {
                return this._startOfWeek.AddDays(TimeslipTopLineIndex);
            }
        }

        public int TimeslipTopLineIndex
        {
            get
            {
                int i = 0;
                while ((i < 7) && (_status[i] == SCRATCHED))
                    i++;
                return i;
            }
        }


        public DateTime StartOfRollWeek
        {
            get
            {
                if (!IsToBeRolledOver)
                    return Utility.NullDateValue;

                DateTime ret = RollOverDate;

                System.DayOfWeek dow = _startOfWeek.DayOfWeek;

                while (ret.DayOfWeek != dow)
                    ret = ret.AddDays(-1.0);

                return ret;
            }
        }

        public List<DayStatus> Draw()
        {
            DayStatus dayStatus = null;
            List<DayStatus> dayStatusList = new List<DayStatus>();

            dayStatusList.Add(new DayStatus { Label = "Su", Type = 1, Index = 0, IsRoll = false });
            dayStatusList.Add(new DayStatus { Label = "Mo", Type = 1, Index = 1, IsRoll = false });
            dayStatusList.Add(new DayStatus { Label = "Tu", Type = 1, Index = 2, IsRoll = false });
            dayStatusList.Add(new DayStatus { Label = "We", Type = 1, Index = 3, IsRoll = false });
            dayStatusList.Add(new DayStatus { Label = "Th", Type = 1, Index = 4, IsRoll = false });
            dayStatusList.Add(new DayStatus { Label = "Fr", Type = 1, Index = 5, IsRoll = false });
            dayStatusList.Add(new DayStatus { Label = "Sa", Type = 1, Index = 6, IsRoll = false });

            dayStatusList.Add(new DayStatus { Label = "Su", Type = 2, Index = 0, IsRoll = true });
            dayStatusList.Add(new DayStatus { Label = "Mo", Type = 2, Index = 1, IsRoll = true });
            dayStatusList.Add(new DayStatus { Label = "Tu", Type = 2, Index = 2, IsRoll = true });
            dayStatusList.Add(new DayStatus { Label = "We", Type = 2, Index = 3, IsRoll = true });
            dayStatusList.Add(new DayStatus { Label = "Th", Type = 2, Index = 4, IsRoll = true });
            dayStatusList.Add(new DayStatus { Label = "Fr", Type = 2, Index = 5, IsRoll = true });
            dayStatusList.Add(new DayStatus { Label = "Sa", Type = 2, Index = 6, IsRoll = true });


            foreach (DayStatus ds in dayStatusList)
                configureLabel(ds);

            return dayStatusList;
        }

        public void DrawAfterClick(List<DayStatus> dayStatusList)
        {
            if (_drawStyle == DrawStyle.Order)
            {
                foreach (DayStatus ds in dayStatusList)
                    configureLabel(ds);
            }
            else
            {
                configureLabels(dayStatusList);
            }

            DayStatusList = dayStatusList;
        }


        private void configureLabel(DayStatus dayStatus)
        {
            string Label = dayStatus.Label;
            int index = dayStatus.Index;
            bool isRoll = dayStatus.IsRoll;

            bool show = !isRoll || (isRoll && IsToBeRolledOver && !IsRollInCurrentWeek);
            dayStatus.Visible = show;
            if (!show)
            {
                dayStatus.Visible = false;
                return;
            }

            DateTime labelDate;

            if (isRoll)
            {
                dayStatus.Status = getStatusNameByIndex(index, isRoll);
                labelDate = StartOfRollWeek.AddDays(Convert.ToDouble(index));
                dayStatus.Enabled = true;
            }
            else
            {
                dayStatus.Status = getStatusNameByIndex(index, isRoll);
                labelDate = StartOfWeek.AddDays(Convert.ToDouble(index));
                dayStatus.Enabled = (BillState == RESERVED) || (getStatusByIndex(index) != SCRATCHED);
            }

            bool labelConfirmedUntilIndicator = false;
            if (Utility.ToEndOfDay(labelDate) == ConfirmedThrough)
            {
                labelConfirmedUntilIndicator = true;
            }
            else
                labelConfirmedUntilIndicator = false;

            dayStatus.IsStarShow = labelConfirmedUntilIndicator;


            bool isConfirmed = Utility.ToEndOfDay(labelDate).Ticks <= ConfirmedThrough.Ticks;
            string style = "Normal";


            dayStatus.Date = labelDate.ToString("dd-MMM-yyyy");

            if (isConfirmed
                            && (
                            (isRoll && (getStatusRollByIndex(index) > TENTATIVE))
                            || (!isRoll && (getStatusByIndex(index) > TENTATIVE))
                            )
                            )
            {
                style = "Bold";
            }

            if (Utility.ToStartOfDay(labelDate).Ticks == Utility.ToStartOfDay(RollOverDate).Ticks)
                style = "Italic";

            dayStatus.FontStyle = style;

            dayStatus.Text = getLabelOfIndex(index, isRoll);

            if (isRoll)
            {
                dayStatus.ForeColor = this.getForeColor(getStatusRollByIndex(index));
                dayStatus.BackColor = this.getBackColor(getStatusRollByIndex(index));
            }
            else
            {
                dayStatus.ForeColor = this.getForeColor(getStatusByIndex(index));
                dayStatus.BackColor = this.getBackColor(getStatusByIndex(index));
            }
        }

        public string getStatusNameByIndex(int index, bool isRoll)
        {
            switch (isRoll ? _statusToRollTo[index] : _status[index])
            {
                case SCRATCHED: return "SCRATCHED";
                case TENTATIVE: return "TENTATIVE";
                case REQUESTED: return "REQUESTED";
                case SENT: return "SENT";
                case OFF: return "OFF";
                case FINISHED: return "FINISHED";
                default: goto case SCRATCHED;
            }
        }

        public byte getStatusByIndex(int index)
        {
            bool isValidated = validateIndex(index);

            if (isValidated)
                return _status[index];
            else
                return 0;
        }

        public byte getStatusRollByIndex(int index)
        {
            bool isValidated = validateIndex(index);

            if (isValidated)
                return _statusToRollTo[index];
            else
                return 0;
        }


        public bool validateIndex(int index)
        {
            if ((index < 0) || (index > 6))
                return false;
            else
                return true;
        }



        public DateTime ConfirmedThrough
        {
            get { return _confirmedThrough; }
            set
            {
                if (value == _confirmedThrough)
                    return;
            }
        }

        public String getLabelOfIndex(int index, bool isRoll)
        {
            return getDateOfIndex(index, isRoll).ToString("ddd").Substring(0, 2);
        }

        public DateTime getDateOfIndex(int index, bool isRoll)
        {
            if (isRoll)
                return this.StartOfRollWeek.AddDays(Convert.ToDouble(index));
            else
                return _startOfWeek.AddDays(Convert.ToDouble(index));
        }


        private string getForeColor(byte status)
        {
            switch (status)
            {
                case TimeslipStatus.SCRATCHED:
                    if (_drawStyle == DrawStyle.Order)
                        return "#696969";  //dim grey
                    else
                        return "#696969";  //gainsboro
                case TimeslipStatus.TENTATIVE: return "#000";
                case TimeslipStatus.REQUESTED: return "#000";
                case TimeslipStatus.SENT: return "#000";
                case TimeslipStatus.OFF: return "#000";
                case TimeslipStatus.FINISHED: return "#000";
                default: goto case TimeslipStatus.SCRATCHED;
            }
        }


        private string getBackColor(byte status)
        {
            switch (status)
            {
                case TimeslipStatus.SCRATCHED: return "Transparent";
                case TimeslipStatus.TENTATIVE:
                    if (_drawStyle == DrawStyle.Order)
                        return "#ffffff";
                    else
                        return "Transparent";
                case TimeslipStatus.REQUESTED: return "#ffff00";
                case TimeslipStatus.SENT: return "#00ff00";
                case TimeslipStatus.OFF: return "#ff0000";
                case TimeslipStatus.FINISHED: return "#000";
                default: goto case TimeslipStatus.SCRATCHED;
            }
        }

        public List<DayStatus> GetRolloverDayStatus(DateTime RollODate)
        {
            if (_drawStyle == DrawStyle.Order)
            {
                RollOverDate = RollODate;
                DayStatusList = Draw();
            }
            else
            {
                RollOverDate = RollODate;
                DayStatusList = DrawTimeslip();
            }
            return DayStatusList;
        }



        public static bool IsRolloverByDefaultOnNewTimeslip(bool isOneDay, String activityRequiredForNewJob)
        {
            bool isPeicework = isOneDay && IsPeiceWork(activityRequiredForNewJob);
            return !isPeicework;
        }

        public static bool IsPeiceWork(String activityName)
        {
            return activityName.ToUpper().StartsWith("LOAD / UNLOAD");
        }

        private byte _selectedLabelInitialStatus = 0;

        public void SetCurrentStatus(List<DayStatus> DayStatusList)
        {
            bool IsRoll = false;
            int Index = 0;
            string Status = string.Empty;

            foreach (DayStatus ds in DayStatusList)
            {
                if (ds.Click)
                {
                    IsRoll = ds.IsRoll;
                    Index = ds.Index;
                    Status = ds.Status;
                }
            }

            bool isRoll = IsRoll;
            int index = Index;
            String currentStatusStr = Status;

            byte currentStatus = isRoll ? getStatusRollByIndex(index) : getStatusByIndex(index);
            _selectedLabelInitialStatus = currentStatus;

            if (!isRoll && (_selectedLabelInitialStatus == SCRATCHED) && (BillState != RESERVED))
                return;

            if (isRoll)
            {
                switch (currentStatus)
                {
                    case SCRATCHED: setStatusToRollToByIndex(index, TENTATIVE); break;
                    case TENTATIVE: setStatusToRollToByIndex(index, REQUESTED); break;
                    case REQUESTED: setStatusToRollToByIndex(index, OFF); break;
                    case SENT: setStatusToRollToByIndex(index, OFF); break;
                    case OFF: setStatusToRollToByIndex(index, FINISHED); break;
                    case FINISHED:
                        if ((_drawStyle == DrawStyle.Timeslip) || isOnlyOneNotScratched(isRoll))
                            setStatusToRollToByIndex(index, TENTATIVE);
                        else
                            setStatusToRollToByIndex(index, SCRATCHED);
                        break;
                    //if (isOnlyOneNotScratched(isRoll))
                    //    setStatusToRollToByIndex(index, TENTATIVE);
                    //else
                    //    setStatusToRollToByIndex(index, SCRATCHED);
                    //break;
                    default: goto case SCRATCHED;
                }
            }
            else
            {
                switch (currentStatus)
                {
                    case SCRATCHED: setStatusByIndex(index, TENTATIVE); break;
                    case TENTATIVE: setStatusByIndex(index, REQUESTED); break;
                    case REQUESTED: setStatusByIndex(index, SENT); break;
                    case SENT: setStatusByIndex(index, OFF); break;
                    case OFF:
                        if ((BillState == RESERVED) && (_drawStyle != DrawStyle.Order))
                            setStatusByIndex(index, TENTATIVE);
                        else
                            setStatusByIndex(index, FINISHED);
                        break;

                    //setStatusByIndex(index, FINISHED);
                    //break;

                    case FINISHED:
                        if (_drawStyle == DrawStyle.Order)
                            setStatusByIndex(index, isOnlyOneNotScratched(isRoll) ? TENTATIVE : SCRATCHED);
                        else
                            setStatusByIndex(index, TENTATIVE); break;

                    //setStatusByIndex(index, isOnlyOneNotScratched(isRoll) ? TENTATIVE : SCRATCHED);
                    //break;

                    default: goto case SCRATCHED;
                }
            }

            DrawAfterClick(DayStatusList);
        }


        public void setStatusByIndex(int index, byte newStatus)
        {
            validateIndex(index);

            _status[index] = newStatus;
        }

        public void setStatusToRollToByIndex(int index, byte newStatus)
        {
            validateIndex(index);

            _statusToRollTo[index] = newStatus;
        }

        public bool isOnlyOneNotScratched(bool isRoll)
        {
            bool foundNotScratchedDay = false;
            for (int i = 0; i < 7; i++)
            {
                if ((isRoll && (_statusToRollTo[i] != SCRATCHED)) || (!isRoll && (_status[i] != SCRATCHED)))
                {
                    if (foundNotScratchedDay)
                        return false;
                    else
                        foundNotScratchedDay = true;
                }
            }
            return true;
        }

        public String Composite
        {
            get
            {
                String ret = String.Concat(
                    "<Stat>", ToStatusStr(_status), Utility.ToDateQB(_confirmedThrough), "</Stat>"
                    , "<Roll>", ToStatusStr(_statusToRollTo), Utility.ToDateQB(_rollOverDate), "</Roll>");

                //ret = ret + "<Top>" + TimeslipFirstDayStr + "^" + RollFirstDayStr + "</Top>";

                ret = String.Concat(ret
                    , "<Week>", Utility.ToDateQB(_startOfWeek), "^", Utility.ToDateQB(_endOfWeek), "</Week>"
                    , "<IsOneDay>", IsOneDay ? "true" : "false", "</IsOneDay>");

                return ret;
            }
        }

        public static String ToStatusStr(byte[] status)
        {
            String ret = String.Empty;

            foreach (byte day in status)
                ret = ret + Convert.ToChar(day + (byte)'0');

            return ret;
        }




        public List<DayStatus> DrawTimeslip()
        {
            DayStatus dayStatus = null;
            List<DayStatus> dayStatusList = new List<DayStatus>();

            dayStatusList.Add(new DayStatus { Label = "Su", Type = 1, Index = 0, IsRoll = false, Visible = false });
            dayStatusList.Add(new DayStatus { Label = "Mo", Type = 1, Index = 1, IsRoll = false, Visible = false });
            dayStatusList.Add(new DayStatus { Label = "Tu", Type = 1, Index = 2, IsRoll = false, Visible = false });
            dayStatusList.Add(new DayStatus { Label = "We", Type = 1, Index = 3, IsRoll = false, Visible = false });
            dayStatusList.Add(new DayStatus { Label = "Th", Type = 1, Index = 4, IsRoll = false, Visible = false });
            dayStatusList.Add(new DayStatus { Label = "Fr", Type = 1, Index = 5, IsRoll = false, Visible = false });
            dayStatusList.Add(new DayStatus { Label = "Sa", Type = 1, Index = 6, IsRoll = false, Visible = false });

            return configureLabels(dayStatusList);
        }


        private List<DayStatus> configureLabels(List<DayStatus> dayStatusList)
        {
            int timeslipTopLineIndex = TimeslipTopLineIndex;
            int timeslipLastLineIndex = getIndexOfDate(LastDayInTimeslip, false);
            bool labelConfirmedUntilIndicator = false;

            for (int i = timeslipTopLineIndex; i <= timeslipLastLineIndex; i++)
            {
                var dayS = dayStatusList.Where(x => x.Index == i).Take(1).SingleOrDefault();

                int timeslipLineNumber = toTimeslipLineNumber(i);

                dayS.Text = getTimeslipLabel(timeslipLineNumber);
                dayS.ForeColor = this.getForeColor(getStatusByIndex(i));
                dayS.BackColor = this.getBackColor(getStatusByIndex(i));
                dayS.Status = getStatusNameByIndex(i, dayS.IsRoll);


                DateTime labelDate = StartOfWeek.AddDays(Convert.ToDouble(i));

                dayS.Date = labelDate.ToString("dd-MMM-yyyy");

                if (Utility.ToEndOfDay(labelDate) == ConfirmedThrough)
                {
                    labelConfirmedUntilIndicator = true;
                }
                else
                    labelConfirmedUntilIndicator = false;

                bool isConfirmed = Utility.ToEndOfDay(labelDate).Ticks <= ConfirmedThrough.Ticks;
                string style = "Normal";

                if (isConfirmed && (getStatusByIndex(i) > TENTATIVE))
                    style = "Bold";

                dayS.FontStyle = style;
                dayS.Visible = true;

                dayS.IsStarShow = labelConfirmedUntilIndicator;

                Index_Recalculate = i;
                IsRoll_Recalculate = dayS.IsRoll;
                PreConfirmThrough = ConfirmedThrough;
            }

            return dayStatusList;
        }

        public static int Index_Recalculate { get; set; }
        public static bool IsRoll_Recalculate { get; set; }
        public static DateTime PreConfirmThrough { get; set; }



        public void getReCalculateConfirmedThrough(List<DayStatus> dayStatusList)
        {
            byte currentStatus = IsRoll_Recalculate ? getStatusRollByIndex(Index_Recalculate) : getStatusByIndex(Index_Recalculate);

            DateTime prevConfirmedThrough = PreConfirmThrough;

            bool wasToBeRolled = IsToBeRolledOver;

            if (currentStatus == FINISHED)
            {
                finish(getDateOfIndex(Index_Recalculate, IsRoll_Recalculate));
            }

            ConfirmedThrough = recalculateConfirmedThrough();

            configureLabels(dayStatusList);
        }


        public DateTime recalculateConfirmedThrough()
        {
            DateTime lastConfirmedDate = Utility.NullDateValue;

            int firstIndexInTimeslip = TimeslipTopLineIndex;
            DateTime lastDayInTimeslip = LastDayInTimeslip;
            bool isRoll = IsToBeRolledOver;
            bool isRollInCurrentWeek = isRoll && IsRollInCurrentWeek;
            int ww = this.DaysInWeek;

            DateTime dayInQuestion = this.getDateOfIndex(firstIndexInTimeslip, false);
            int i = 0;
            for (i = TimeslipTopLineIndex; ((dayInQuestion <= lastDayInTimeslip) || (isRollInCurrentWeek)) && (i < ww); i++)
            {
                if ((_status[i] != TENTATIVE) && (_status[i] != FINISHED)) // includes Scratched, OFF, REQU, SENT
                {
                    lastConfirmedDate = dayInQuestion;
                    dayInQuestion = dayInQuestion.AddDays(1.0);
                }
                else
                { // TENTATIVE or FINISHED marks the last day of confirmation
                    if (_status[i] == FINISHED)
                        _confirmedThrough = Utility.ToEndOfDay(dayInQuestion);
                    else
                        _confirmedThrough = Utility.ToEndOfDay(lastConfirmedDate);
                    return _confirmedThrough;
                }
            }
            if (!isRoll) // && ((dayInQuestion >= lastDayInTimeslip) || (i == ww)))
            {
                if (_statusToRollTo[0] == FINISHED) // user has specifically confirmed the timeslip as finished.
                    lastConfirmedDate = lastConfirmedDate.AddDays(1.0); // we've confirmed to the last day and the user also confirmed we don't rollover the next day
            }
            else
            {
                if (isRollInCurrentWeek) // ie. we've reached the last day in the rollWeek (which was all contained within the current week)
                {
                    if (_statusToRollTo[0] == FINISHED) // has the user specifically marked the timeslip as Finished?
                        lastConfirmedDate = lastConfirmedDate.AddDays(1.0); // confirm including the day after the last day in timeslip to enable the UI_RolloverDailySales RollOver button

                    if (_confirmedThrough <= EndOfWeek.AddDays(8.0)) // if the callback time set to way in the future (ie. after the day after next week) - don't mess with it
                        _confirmedThrough = Utility.ToEndOfDay(lastConfirmedDate);

                    return _confirmedThrough;
                }
                else// if (isRoll)
                {
                    dayInQuestion = RollOverDate;
                    int ri = 0;
                    for (ri = getIndexOfDate(dayInQuestion, true); ri < ww; ri++)
                        if (_statusToRollTo[ri] != TENTATIVE)
                        {
                            lastConfirmedDate = dayInQuestion;
                            dayInQuestion = dayInQuestion.AddDays(1.0);
                        }
                        else
                        {
                            if (_statusToRollTo[ri] == FINISHED)
                                _confirmedThrough = Utility.ToEndOfDay(dayInQuestion);
                            else
                                _confirmedThrough = Utility.ToEndOfDay(lastConfirmedDate);
                            return _confirmedThrough;
                        }
                    if (ri == ww) // ie. we've reached the last day in the rollWeek; if the callback time is way in the future - don't mess with it
                    {
                        if (_confirmedThrough <= EndOfWeek.AddDays(8.0)) // if the callback time set to way in the future (ie. after the day after next week) - don't mess with it
                            _confirmedThrough = Utility.ToEndOfDay(lastConfirmedDate);

                        return _confirmedThrough;
                    }
                }
            }
            _confirmedThrough = Utility.ToEndOfDay(lastConfirmedDate);
            return _confirmedThrough;
        }


        public void finish(DateTime dayAfterItsDone)
        {
            DateTime f = Utility.ToStartOfDay(dayAfterItsDone);

            if ((f.Ticks < _startOfWeek.Ticks)
                || (!IsToBeRolledOver && (f.Ticks > _endOfWeek.Ticks))
                || (IsToBeRolledOver && (f.Ticks > EndOfRollWeek.Ticks)))
            { }

            int s = getIndexOfDate(f, false);
            if (s < 0)
                s = 0; // it finished before it started - mark all current status as FINISHED

            int ww = DaysInWeek;
            if (s < ww) // it finished during the current week
            {
                for (int i = s; i < ww; i++)
                    if (_status[i] != SCRATCHED)
                        _status[i] = FINISHED;

                if (IsToBeRolledOver)// && (f.Ticks > _endOfWeek.Ticks) && (f.Ticks <= RollOverDate.Ticks))
                    RollOverDate = Utility.NullDateValue;
            }
            else
            {
                s = getIndexOfDate(f, true);
                if ((s >= 0) && (s < ww)) // if finishes within the period we will roll to
                {
                    for (int i = s; i < ww; i++) // assume days in rollto week = current week
                        if (_statusToRollTo[i] != SCRATCHED)
                            _statusToRollTo[i] = FINISHED;
                }
                if (IsToBeRolledOver)
                {
                    int firstNonScratchedDayInRoll = ww;
                    for (firstNonScratchedDayInRoll = 0; firstNonScratchedDayInRoll < ww; firstNonScratchedDayInRoll++)
                        if (this.getStatusRollByIndex(firstNonScratchedDayInRoll) != SCRATCHED)
                            break;
                    if ((s <= firstNonScratchedDayInRoll) || (firstNonScratchedDayInRoll == ww))
                        RollOverDate = Utility.NullDateValue;
                }
            }
            _statusToRollTo[0] = FINISHED;
        }

        public DateTime EndOfRollWeek
        {
            get
            {
                if (!IsToBeRolledOver) { }

                DateTime ret = RollOverDate;

                System.DayOfWeek dow = _endOfWeek.DayOfWeek;

                while (ret.DayOfWeek != dow)
                    ret = ret.AddDays(1.0);

                return ret;
            }
        }

        public DateTime LastDayInTimeslip
        {
            get
            {
                DateTime ret = this._startOfWeek.AddDays(6.0);
                int i = 6;
                while ((i >= 0) && (_status[i] == SCRATCHED))
                {
                    ret = ret.AddDays(-1.0);
                    i--;
                }
                //if (i == 0)
                //    throw new rsvLib.Resources.RsvException("UI_StatusWeek: Last Day in Timeslip cannot be calculated when all days are Scratched.", rsvLib.Resources.ExceptionCause.AssertError);

                return ret;
            }
        }

        public int toTimeslipLineNumber(int index)
        {
            return toTimeslipDOW(index) + 1;
        }

        public int toTimeslipDOW(int index)
        {
            validateIndex(index);

            int daysFromFirstDay = index - TimeslipTopLineIndex;
            if (IsOneDay)
            {
                if ((daysFromFirstDay > 0) || (daysFromFirstDay < 2))
                    daysFromFirstDay = daysFromFirstDay * 2;
            }
            return daysFromFirstDay;
        }

        public String getTimeslipLabel(int timeslipLineNumber)
        {
            return getDateOfIndex(toIndex(timeslipLineNumber), false).ToString("ddd").ToUpper();
        }

        public int toIndex(int timeslipLineNumber)
        {
            int index = -1;
            if (IsOneDay)
            {
                switch (timeslipLineNumber)
                {
                    case 1: timeslipLineNumber = 1; break;
                    case 3: timeslipLineNumber = 2; break;
                    case 5: timeslipLineNumber = 3; break;
                    default: goto case 1;
                }
            }
            index = TimeslipTopLineIndex + timeslipLineNumber - 1;

            //if ((index < 6) || (index > 0))
            //    throw new rsvLib.Resources.RsvException("UI_StatusWeek: Illegal TimeslipLine #", rsvLib.Resources.ExceptionCause.AssertError);
            return index;
        }


    }
}
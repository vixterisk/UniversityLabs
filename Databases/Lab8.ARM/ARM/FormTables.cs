using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARM
{
    public partial class FormTables : Form
    {
        public static Dictionary<int, string> cpuDict = new Dictionary<int, string>();
        public static Dictionary<string, int> cpuIdsDict = new Dictionary<string, int>();
        public static Dictionary<int, string> hddDict = new Dictionary<int, string>();
        public static Dictionary<string, int> hddIdsDict = new Dictionary<string, int>();
        public static Dictionary<int, string> makersDict = new Dictionary<int, string>();
        public static Dictionary<string, int> makersIdsDict = new Dictionary<string, int>();
        public static Dictionary<int, string> ddrDict = new Dictionary<int, string>();
        public static Dictionary<string, int> ddrIdsDict = new Dictionary<string, int>();
        public static Dictionary<int, string> caseDict = new Dictionary<int, string>();
        public static Dictionary<string, int> caseIdsDict = new Dictionary<string, int>();
        public static Dictionary<int, string> pcDict = new Dictionary<int, string>();
        public static Dictionary<string, int> pcIdsDict = new Dictionary<string, int>();
        public static Dictionary<int, string> ramDict = new Dictionary<int, string>();
        public static Dictionary<string, int> ramIdsDict = new Dictionary<string, int>();
        FormType currentUser;
        public enum FormType
        {
            adminUser,
            operatorUser,
            guestUser
        }
        public FormTables()
        {
            InitializeComponent();
        }
        public FormTables(FormType frmType)
        {
            InitializeComponent();
            currentUser = frmType;
            if (currentUser != FormType.adminUser)
                tablesTab.TabPages.Remove(userPage);
            else
                InitializeUsersTable();
            InitializeForm();
            TableEditMode(currentUser == FormType.guestUser);
        }

        private void TableEditMode(bool isGuest)
        {
            pcInformationDGV.ReadOnly = isGuest;
            pcInformationDGV.AllowUserToDeleteRows = !isGuest;
            pcInformationDGV.AllowUserToAddRows = !isGuest;
            ramInformationDGV.ReadOnly = isGuest;
            ramInformationDGV.AllowUserToDeleteRows = !isGuest;
            ramInformationDGV.AllowUserToAddRows = !isGuest;
            ddrDGV.ReadOnly = isGuest;
            ddrDGV.AllowUserToDeleteRows = !isGuest;
            ddrDGV.AllowUserToAddRows = !isGuest;
            pcRamDGV.ReadOnly = isGuest;
            pcRamDGV.AllowUserToDeleteRows = !isGuest;
            pcRamDGV.AllowUserToAddRows = !isGuest;
        }

        void InitializeForm()
        {            
            FillDictionaries();
            InitializeDdrDGV();
            InitializeRamInformationDGV();
            InitializePcInformationDGV();
            InitializePcRamDGV();
        }

        public static void FillDictionaries()
        {
            Dictionary<int, Tuple<string, int?>> cpuCoreDict = new Dictionary<int, Tuple<string, int?>>();
            Dictionary<Tuple<string, int?>, int> cpuCoreIDsDict = new Dictionary<Tuple<string, int?>, int>();
            cpuDict = new Dictionary<int, string>();
            cpuIdsDict = new Dictionary<string, int>();
            hddDict = new Dictionary<int, string>();
            hddIdsDict = new Dictionary<string, int>();
            makersDict = new Dictionary<int, string>();
            makersIdsDict = new Dictionary<string, int>();
            ddrDict = new Dictionary<int, string>();
            ddrIdsDict = new Dictionary<string, int>();
            Dictionary<int, string> caseFormFactorDict = new Dictionary<int, string>();
            Dictionary<string, int> caseFormFactorIdsDict = new Dictionary<string, int>();
            caseDict = new Dictionary<int, string>();
            caseIdsDict = new Dictionary<string, int>();
            pcDict = new Dictionary<int, string>();
            pcIdsDict = new Dictionary<string, int>();
            ramDict = new Dictionary<int, string>();
            ramIdsDict = new Dictionary<string, int>();
            using (var pc = new pcModel())
            {
                foreach (var ddr in pc.ddr_generation)
                {
                    ddrDict[ddr.generation_id] = ddr.generation_name;
                    ddrIdsDict[ddr.generation_name] = ddr.generation_id;
                }
                foreach (var maker in pc.maker)
                {
                    makersIdsDict[maker.maker_name] = maker.maker_id;
                    makersDict[maker.maker_id] = maker.maker_name;
                }
                foreach (var cpuCore in pc.cpu_core_information)
                {
                    var tuple = new Tuple<string, int?>(cpuCore.core_name, cpuCore.cpu_core_maker_id);
                    cpuCoreDict[cpuCore.core_id] = tuple;
                    cpuCoreIDsDict[tuple] = cpuCore.core_id;
                }
                foreach (var cpu in pc.cpu_information)
                {
                    var tuple = cpuCoreDict[(int)cpu.core_id];
                    var str = cpu.cpu_model + " by " + makersDict[(int)cpu.cpu_maker_id] + ", Core - " + tuple.Item1 + " by " + makersDict[(int)tuple.Item2];
                    cpuDict[cpu.cpu_id] = str;
                    cpuIdsDict[str] = cpu.cpu_id;
                }
                foreach (var hdd in pc.hdd_information)
                {
                    var str = hdd.hdd_model + " by " + makersDict[(int)hdd.hdd_maker_id] + ", " + hdd.hdd_memory_size + " GB";
                    hddDict[hdd.hdd_id] = str;
                    hddIdsDict[str] = hdd.hdd_id;
                }
                foreach (var formFactor in pc.case_form_factor)
                {
                    caseFormFactorDict[formFactor.case_form_factor_id] = formFactor.form_factor;
                    caseFormFactorIdsDict[formFactor.form_factor] = formFactor.case_form_factor_id;
                }
                foreach (var pcCase in pc.case_information)
                {
                    var str = pcCase.case_model + " (" + caseFormFactorDict[pcCase.case_form_factor_id] + ")" + " by " + makersDict[(int)pcCase.case_maker_id];
                    caseDict[pcCase.case_id] = str;
                    caseIdsDict[str] = pcCase.case_id;
                }
                foreach (var pc_inf in pc.pc_information)
                {
                    var str = "ЦП: " + cpuDict[(int)pc_inf.cpu_id] + ", ЖД: " + hddDict[(int)pc_inf.hdd_id] + ", Корпус: " + caseDict[(int)pc_inf.case_id];
                    pcDict[pc_inf.pc_id] = str;
                    pcIdsDict[str] = pc_inf.pc_id;
                }
                foreach (var ram_inf in pc.ram_information)
                {
                    var str = ram_inf.ram_model + ", " + ram_inf.ram_memory_size + " GB by " + makersDict[(int)ram_inf.ram_maker_id];
                    ramDict[ram_inf.ram_id] = str;
                    ramIdsDict[str] = ram_inf.ram_id;
                }
            }
        }

        private void IntelPcQueryButton_Click(object sender, EventArgs e)
        {
            var intelPcQueryForm = new QueryForm(QueryForm.FormType.Intel);
            intelPcQueryForm.ShowDialog();
        }

        private void DDR4RamButton_Click(object sender, EventArgs e)
        {
            var DDR4RamQueryForm = new QueryForm(QueryForm.FormType.DDR4);
            DDR4RamQueryForm.ShowDialog();
        }
    }
}

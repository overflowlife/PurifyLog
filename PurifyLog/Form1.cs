using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Diagnostics;

namespace PurifyLog
{
    public partial class Form1 : Form
    {
        private string saveFile = "./Countdata.json";
        private string lockFile = "./Countdata.json.lck";
        private Countdata data;

        private FileStream locker;

        private Stopwatch sw_akelon_killed;
        private Stopwatch sw_akelon_mineral;
        private Stopwatch sw_akelon_shard;
        private Stopwatch sw_akelon_parchment;
        private Stopwatch sw_akelon_es;

        private Stopwatch sw_tiaga_killed;
        private Stopwatch sw_tiaga_mineral;
        private Stopwatch sw_tiaga_chain;
        private Stopwatch sw_tiaga_parchment;
        private Stopwatch sw_tiaga_es;
        

        public Form1()
        {
            data = null;
            locker = null;

            InitializeComponent();
            InitializeStopWatch();

            //Windowサイズの固定
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;


            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            
            LoadData();
            ResetAllStopWatch();

            timer.Start();

        }

        private void InitializeStopWatch()
        {
            sw_akelon_killed = new Stopwatch();
            sw_akelon_mineral = new Stopwatch();
            sw_akelon_shard = new Stopwatch();
            sw_akelon_parchment = new Stopwatch();
            sw_akelon_es = new Stopwatch();

            sw_tiaga_killed = new Stopwatch();
            sw_tiaga_mineral = new Stopwatch();
            sw_tiaga_chain = new Stopwatch();
            sw_tiaga_parchment = new Stopwatch();
            sw_tiaga_es = new Stopwatch();
        }

        private void ResetAllStopWatch()
        {
            sw_akelon_killed.Reset();
            sw_akelon_mineral.Reset();
            sw_akelon_shard.Reset();
            sw_akelon_parchment.Reset();
            sw_akelon_es.Reset();

            sw_tiaga_killed.Reset();
            sw_tiaga_mineral.Reset();
            sw_tiaga_chain.Reset();
            sw_tiaga_parchment.Reset();
            sw_tiaga_es.Reset();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            updateTimer();
        }

        /// <summary>
        /// saveFileから内容読み取り
        /// </summary>
        private void LoadData()
        {
            if (File.Exists(saveFile))
            {
                if (File.Exists(lockFile))
                {
                    bool liveLockFile = true;
                    try
                    {
                        File.Delete(lockFile);
                        liveLockFile = false;                        
                    }
                    catch (Exception)
                    {
                        //lockファイル削除を試行したが失敗した
                    }
                    if (liveLockFile)
                    {
                        MessageBox.Show($"同一データを使用するインスタンスがすでに実行中のようです。\n否定する場合は、以下のlockファイルを確認してください。\n{Path.GetFullPath(lockFile)}\n終了します。", "浄化記録システム");
                        this.Close();
                        return;
                    }
                }

                try
                {
                    locker = File.Open(lockFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                }
                catch (Exception)
                {
                    MessageBox.Show("lockファイル生成に失敗しました。終了します。", "浄化記録システム");
                    this.Close();
                    return;
                }


                try
                {
                    data = JsonConvert.DeserializeObject<Countdata>(File.ReadAllText(saveFile));
                }
                catch (Exception)
                {
                    MessageBox.Show("データファイルオープンに失敗しました。終了します。");
                    this.Close();
                    return;
                }
                
                if(data is null)
                {
                    MessageBox.Show($"データの読み込みに失敗しました。\n以下のファイル内容を確認するか、管理者に解決を依頼してください。\n{Path.GetFullPath(saveFile)}\n終了します。", "浄化記録システム");
                    this.Close();
                    return;
                }

            }
            else
            {
                MessageBox.Show("データが存在しないため初期化します。", "浄化記録システム");
                data = new Countdata();
            }

            akelon_killed.Value = data.akelon.killed;
            akelon_mineral.Value = data.akelon.mineral;
            akelon_shard.Value = data.akelon.material;
            akelon_parchment.Value = data.akelon.parchment;
            akelon_es.Value = data.akelon.es;

            tiaga_killed.Value = data.tiaga.killed;
            tiaga_mineral.Value = data.tiaga.mineral;
            tiaga_chain.Value = data.tiaga.material;
            tiaga_parchment.Value = data.tiaga.parchment;
            tiaga_es.Value = data.tiaga.es;

            krieg_killed.Value = data.krieg.killed;
            krieg_mineral.Value = data.krieg.mineral;
            krieg_slate.Value = data.krieg.material;
            krieg_parchment.Value = data.krieg.parchment;
            krieg_es.Value = data.krieg.es;

        }

        #region add button
        private void button1_Click(object sender, EventArgs e)
        {
            akelon_killed.Value++;
            sw_akelon_killed.Restart();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            akelon_killed.Value += 4;
            sw_akelon_killed.Restart();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            akelon_killed.Value += 8;
            sw_akelon_killed.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            akelon_mineral.Value++;
            sw_akelon_mineral.Restart(); ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            akelon_mineral.Value += 2;
            sw_akelon_mineral.Restart();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            akelon_mineral.Value += 3;
            sw_akelon_mineral.Restart();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            akelon_shard.Value++;
            sw_akelon_shard.Restart();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            akelon_shard.Value+=2;
            sw_akelon_shard.Restart();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            akelon_parchment.Value++;
            sw_akelon_parchment.Restart();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            akelon_es.Value++;
            sw_akelon_es.Restart();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            tiaga_killed.Value++;
            sw_tiaga_killed.Restart();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            tiaga_killed.Value += 4;
            sw_tiaga_killed.Restart();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            tiaga_killed.Value += 8;
            sw_tiaga_killed.Restart();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            tiaga_mineral.Value++;
            sw_tiaga_mineral.Restart();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tiaga_mineral.Value += 2;
            sw_tiaga_mineral.Restart();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tiaga_mineral.Value += 3;
            sw_tiaga_mineral.Restart();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tiaga_chain.Value++;
            sw_tiaga_chain.Restart();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            tiaga_chain.Value+=2;
            sw_tiaga_chain.Restart();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tiaga_parchment.Value++;
            sw_tiaga_parchment.Restart();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tiaga_es.Value++;
            sw_tiaga_es.Restart();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            krieg_killed.Value++;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            krieg_killed.Value += 4;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            krieg_killed.Value += 8;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            krieg_mineral.Value++;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            krieg_mineral.Value += 2;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            krieg_mineral.Value += 3;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            krieg_slate.Value++;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            krieg_parchment.Value++;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            krieg_es.Value++;
        }

        private void updateSumKilled()
        {
            sum_killed.Value = akelon_killed.Value + tiaga_killed.Value + krieg_killed.Value;
        }

        private void updateSumMineral()
        {

            sum_mineral.Value = akelon_mineral.Value + tiaga_mineral.Value + krieg_mineral.Value;
        }

        private void updateSumMaterial()
        {
            sum_material.Value = akelon_shard.Value + tiaga_chain.Value + krieg_slate.Value;
        }

        private void updateSumParchment()
        {
            sum_parchment.Value = akelon_parchment.Value + tiaga_parchment.Value + krieg_parchment.Value;
        }

        private void updateSumEs()
        {
            sum_es.Value = akelon_es.Value + tiaga_es.Value + krieg_es.Value;
        }

        private void akelon_killed_ValueChanged(object sender, EventArgs e)
        {
            updateSumKilled();
            sw_akelon_killed.Restart();
        }

        private void tiaga_killed_ValueChanged(object sender, EventArgs e)
        {
            updateSumKilled();
            sw_tiaga_killed.Restart();
        }

        private void krieg_killed_ValueChanged(object sender, EventArgs e)
        {
            updateSumKilled();
        }

        private void akelon_mineral_ValueChanged(object sender, EventArgs e)
        {
            updateSumMineral();
            sw_akelon_mineral.Restart();
        }

        private void tiaga_mineral_ValueChanged(object sender, EventArgs e)
        {
            updateSumMineral();
            sw_tiaga_mineral.Restart();
        }

        private void krieg_mineral_ValueChanged(object sender, EventArgs e)
        {
            updateSumMineral();
        }

        private void akelon_shard_ValueChanged(object sender, EventArgs e)
        {
            updateSumMaterial();
            sw_akelon_shard.Restart();
        }

        private void tiaga_chain_ValueChanged(object sender, EventArgs e)
        {
            updateSumMaterial();
            sw_tiaga_chain.Restart();
        }

        private void krieg_slate_ValueChanged(object sender, EventArgs e)
        {
            updateSumMaterial();
        }

        private void akelon_parchment_ValueChanged(object sender, EventArgs e)
        {
            updateSumParchment();
            sw_akelon_parchment.Restart();
        }

        private void tiaga_parchment_ValueChanged(object sender, EventArgs e)
        {
            updateSumParchment();
            sw_tiaga_parchment.Restart();
        }

        private void krieg_parchment_ValueChanged(object sender, EventArgs e)
        {
            updateSumParchment();
        }

        private void akelon_es_ValueChanged(object sender, EventArgs e)
        {
            updateSumEs();
            sw_akelon_es.Restart();
        }

        private void tiaga_es_ValueChanged(object sender, EventArgs e)
        {
            updateSumEs();
            sw_tiaga_es.Restart();
        }

        private void krieg_es_ValueChanged(object sender, EventArgs e)
        {
            updateSumEs();
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 終了確認します。Yesで保存して終了、Noで保存しないで終了、Cancelで画面に戻ります。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (MessageBox.Show("保存して終了しますか？\n\tはい..保存＆終了\n\tいいえ..終了\n\tキャンセル..戻る", "浄化記録システム", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes:
                    SaveData();
                    e.Cancel = false;
                    try
                    {
                        locker.Dispose();
                        locker.Close();
                        File.Delete(lockFile);
                    }catch {/*削除失敗*/ }
                    
                    break;
                case DialogResult.No:
                    e.Cancel = false;
                    try
                    {
                        locker.Dispose();
                        locker.Close();
                        File.Delete(lockFile);
                    }
                    catch {/*削除失敗*/ }
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            data.akelon.killed = akelon_killed.Value;
            data.akelon.mineral = akelon_mineral.Value;
            data.akelon.material = akelon_shard.Value;
            data.akelon.parchment = akelon_parchment.Value;
            data.akelon.es = akelon_es.Value;

            data.tiaga.killed = tiaga_killed.Value;
            data.tiaga.mineral = tiaga_mineral.Value;
            data.tiaga.material = tiaga_chain.Value;
            data.tiaga.parchment = tiaga_parchment.Value;
            data.tiaga.es = tiaga_es.Value;

            data.krieg.killed = krieg_killed.Value;
            data.krieg.mineral = krieg_mineral.Value;
            data.krieg.material = krieg_slate.Value;
            data.krieg.parchment = krieg_parchment.Value;
            data.krieg.es = krieg_es.Value;

            string savedata = JsonConvert.SerializeObject(data);
            try
            {
                File.WriteAllText(saveFile, savedata);
                MessageBox.Show("データ保存完了", "浄化記録システム");
            }
            catch (Exception e )
            {
                MessageBox.Show(e.Message, "Error");
                string tempSaveFile = "./emergency_countdata.json";
                MessageBox.Show($"データ保存に失敗しました。緊急保存先として以下ファイルに保存を試みます。\n{Path.GetFullPath(tempSaveFile)}", "浄化記録システム");
                File.WriteAllText(tempSaveFile, savedata);
            }

        }

 

        private void updateTimer()
        {
            timer_akelon_killcount.Text = $"最終更新：{(int)sw_akelon_killed.Elapsed.TotalMinutes}分{sw_akelon_killed.Elapsed.Seconds}秒前";
            timer_akelon_mineral.Text = $"最終更新：{(int)sw_akelon_mineral.Elapsed.TotalMinutes}分{sw_akelon_mineral.Elapsed.Seconds}秒前";
            timer_akelon_shard.Text = $"最終更新：{(int)sw_akelon_shard.Elapsed.TotalMinutes}分{sw_akelon_shard.Elapsed.Seconds}秒前";
            timer_akelon_parchment.Text = $"最終更新：{(int)sw_akelon_parchment.Elapsed.TotalMinutes}分{sw_akelon_parchment.Elapsed.Seconds}秒前";
            timer_akelon_es.Text = $"最終更新：{(int)sw_akelon_es.Elapsed.TotalMinutes}分{sw_akelon_es.Elapsed.Seconds}秒前";

            timer_tiaga_killcount.Text = $"最終更新：{(int)sw_tiaga_killed.Elapsed.TotalMinutes}分{sw_tiaga_killed.Elapsed.Seconds}秒前";
            timer_tiaga_mineral.Text = $"最終更新：{(int)sw_tiaga_mineral.Elapsed.TotalMinutes}分{sw_tiaga_mineral.Elapsed.Seconds}秒前";
            timer_tiaga_chain.Text = $"最終更新：{(int)sw_tiaga_chain.Elapsed.TotalMinutes}分{sw_tiaga_chain.Elapsed.Seconds}秒前";
            timer_tiaga_parchment.Text = $"最終更新：{(int)sw_tiaga_parchment.Elapsed.TotalMinutes}分{sw_tiaga_parchment.Elapsed.Seconds}秒前";
            timer_tiaga_es.Text = $"最終更新：{(int)sw_tiaga_es.Elapsed.TotalMinutes}分{sw_tiaga_es.Elapsed.Seconds}秒前";
        }

        private void timer_akelon_killcount_Click(object sender, EventArgs e)
        {
            sw_akelon_killed.Reset();
        }

        private void timer_akelon_mineral_Click(object sender, EventArgs e)
        {
            sw_akelon_mineral.Reset();
        }

        private void timer_akelon_shard_Click(object sender, EventArgs e)
        {
            sw_akelon_shard.Reset();
        }

        private void timer_akelon_parchment_Click(object sender, EventArgs e)
        {
            sw_akelon_parchment.Reset();
        }

        private void timer_akelon_es_Click(object sender, EventArgs e)
        {
            sw_akelon_es.Reset();
        }

        private void timer_tiaga_killcount_Click(object sender, EventArgs e)
        {
            sw_tiaga_killed.Reset();
        }

        private void timer_tiaga_mineral_Click(object sender, EventArgs e)
        {
            sw_tiaga_mineral.Reset();
        }

        private void timer_tiaga_chain_Click(object sender, EventArgs e)
        {
            sw_tiaga_chain.Reset();
        }

        private void timer_tiaga_parchment_Click(object sender, EventArgs e)
        {
            sw_tiaga_parchment.Reset();
        }

        private void timer_tiaga_es_Click(object sender, EventArgs e)
        {
            sw_tiaga_es.Reset();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            string name = "ティアガ報酬入手数";
            int count = (int)tiaga_killed.Value;
            textBox_notify.Text = $"{name}：{count}";
        }

        private void label9_Click(object sender, EventArgs e)
        {
            string name = "神聖な鉱物の欠片（ティアガ）";
            decimal mole = tiaga_mineral.Value;
            decimal deno = tiaga_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;

            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label8_Click(object sender, EventArgs e)
        {
            string name = "青い棘の足かせ";
            decimal mole = tiaga_chain.Value;
            decimal deno = tiaga_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;

            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label7_Click(object sender, EventArgs e)
        {
            string name = "破れた羊皮紙（ティアガ）";
            decimal mole = tiaga_parchment.Value;
            decimal deno = tiaga_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;

            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label6_Click(object sender, EventArgs e)
        {
            string name = "専用ES（孤独）";
            decimal mole = tiaga_es.Value;
            decimal deno = tiaga_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;

            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label_akelon_killed_Click(object sender, EventArgs e)
        {
            string name = "アケロン報酬入手数";
            int count = (int)akelon_killed.Value;
            textBox_notify.Text = $"{name}：{count}";
        }

        private void label_akelon_mineral_Click(object sender, EventArgs e)
        {
            string name = "神聖な鉱物の欠片（アケロン）";
            decimal mole = akelon_mineral.Value;
            decimal deno = akelon_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;


            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label_akelon_shard_Click(object sender, EventArgs e)
        {
            string name = "粉々になった甲羅";
            decimal mole = akelon_shard.Value;
            decimal deno = akelon_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;

            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label_akelon_parchment_Click(object sender, EventArgs e)
        {
            string name = "破れた羊皮紙（アケロン）";
            decimal mole = akelon_parchment.Value;
            decimal deno = akelon_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;

            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label_akelon_es_Click(object sender, EventArgs e)
        {
            string name = "専用ES（珍しい）";
            decimal mole = akelon_es.Value;
            decimal deno = akelon_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;

            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void button_clipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_notify.Text)){
                Clipboard.SetText(textBox_notify.Text);
            }
            
        }

        private void label20_Click(object sender, EventArgs e)
        {
            string name = "神聖な鉱物の欠片";
            decimal mole = sum_mineral.Value;
            decimal deno = sum_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;

            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label19_Click(object sender, EventArgs e)
        {
            string name = "甲羅/足かせ/石板";
            decimal mole = sum_material.Value;
            decimal deno = sum_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;

            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label18_Click(object sender, EventArgs e)
        {
            string name = "破れた羊皮紙";
            decimal mole = sum_parchment.Value;
            decimal deno = sum_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;

            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label17_Click(object sender, EventArgs e)
        {
            string name = "専用ES（珍しい/孤独/誘因）";
            decimal mole = sum_es.Value;
            decimal deno = sum_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;

            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label15_Click(object sender, EventArgs e)
        {
            string name = "クリーグ報酬入手数";
            int count = (int)krieg_killed.Value;
            textBox_notify.Text = $"{name}：{count}";
        }

        private void label14_Click(object sender, EventArgs e)
        {
            string name = "神聖な鉱物の欠片（クリーグ）";
            decimal mole = krieg_mineral.Value;
            decimal deno = krieg_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;


            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label13_Click(object sender, EventArgs e)
        {
            string name = "魔力が宿った石板";
            decimal mole = krieg_slate.Value;
            decimal deno = krieg_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;


            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label12_Click(object sender, EventArgs e)
        {
            string name = "破れた羊皮紙（クリーグ）";
            decimal mole = krieg_parchment.Value;
            decimal deno = krieg_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;


            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label11_Click(object sender, EventArgs e)
        {
            string name = "専用ES（誘因）";
            decimal mole = krieg_es.Value;
            decimal deno = krieg_killed.Value;
            if (deno == 0)
            {
                textBox_notify.Text = "計算不能（div/0）";
                return;
            }
            decimal percent = (mole / deno) * 100;


            textBox_notify.Text = $"{name}：約{Math.Round(percent, 2)}% （{(int)mole} / {(int)deno}）";
        }

        private void label16_Click(object sender, EventArgs e)
        {
            {
                string name = "浄化報酬入手数";
                int count = (int)sum_killed.Value;
                textBox_notify.Text = $"{name}：{count}";
            }
        }
    }
}

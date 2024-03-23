using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class worseCodeScript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable ModuleSelectable;

    public GameObject LED;
    public KMSelectable LEDSel;
    public KMSelectable Submit;
    public KMSelectable[] Buttons;
    public TextMesh[] ButtonLabels;
    public GameObject[] ButtonLabelObjs;
    public KMSelectable[] LaneMomentSels;
    public SpriteRenderer[] LaneMomentSprs;
    public Sprite[] Sprites;
    public Color[] Colors;

    float duration = 0.5f;
    bool assistMode = false;
    string ass = "QWOP";
    bool holding = false;
    bool submissionPhase = false;
    bool focused = false;
    string bet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    string[] morseBet = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
    string cw = "urdl";
    string[] orders = { "urdl", "urld", "udrl", "udlr", "ulrd", "uldr", "rudl", "ruld", "rdul", "rdlu", "rlud", "rldu", "durl", "dulr", "drul", "drlu", "dlur", "dlru", "lurd", "ludr", "lrud", "lrdu", "ldur", "ldru" };
    string[] paths = { "uuulu", "uuuruuld", "uuururu", "uuurul", "uuul", "uuuuuldld", "uuuurdr", "uuuuullddru", "uuuuld", "uuuluurd", "uuurur", "uuululd", "uuuurd", "uuuru", "uuuurrdl", "uuuluru", "uuuuurdrd", "uuulul", "uuuulldr", "uuur", "uuuuldl", "uuuuulldrd", "uuulur", "uuurulu", "uuururd", "uuuurdru" };
    string[] impossiblePuzzles = { "BCFG", "BCFH", "BCFL", "BCFP", "BCFZ", "BCGH", "BCGL", "BCGP", "BCGZ", "BCHL", "BCHP", "BCHQ", "BCHX", "BCHZ", "BCLP", "BCLZ", "BCPZ", "BDHJ", "BDHL", "BDHQ", "BDHY", "BDHZ", "BDJL", "BDJQ", "BDJY", "BDJZ", "BDLQ", "BDLY", "BDLZ", "BDQY", "BDQZ", "BDYZ", "BFGH", "BFGL", "BFGP", "BFGZ", "BFHL", "BFHP", "BFHZ", "BFLP", "BFLZ", "BFPZ", "BGHL", "BGHP", "BGHZ", "BGLP", "BGLZ", "BGPZ", "BHJL", "BHJQ", "BHJY", "BHJZ", "BHKQ", "BHKX", "BHLO", "BHLP", "BHLQ", "BHLR", "BHLS", "BHLX", "BHLY", "BHLZ", "BHOQ", "BHOZ", "BHPQ", "BHPX", "BHPZ", "BHQS", "BHQW", "BHQX", "BHQY", "BHQZ", "BHRZ", "BHSZ", "BHXZ", "BHYZ", "BJLQ", "BJLY", "BJLZ", "BJQY", "BJQZ", "BJYZ", "BLOZ", "BLPZ", "BLQY", "BLQZ", "BLRZ", "BLSZ", "BLXZ", "BLYZ", "BQYZ", "CFGH", "CFGL", "CFGP", "CFGZ", "CFHL", "CFHP", "CFHV", "CFHZ", "CFLP", "CFLZ", "CFPZ", "CGHL", "CGHP", "CGHZ", "CGLP", "CGLZ", "CGPZ", "CHJK", "CHJL", "CHJV", "CHJX", "CHJY", "CHKV", "CHKX", "CHLP", "CHLZ", "CHPV", "CHPZ", "CHQX", "CHVX", "CHVY", "CHXY", "CHXZ", "CJVX", "CJVY", "CJXY", "CLPZ", "CVXY", "DHJL", "DHJQ", "DHJY", "DHJZ", "DHLQ", "DHLY", "DHLZ", "DHQY", "DHQZ", "DHYZ", "DJLQ", "DJLY", "DJLZ", "DJQY", "DJQZ", "DJYZ", "DLQY", "DLQZ", "DLYZ", "DQYZ", "FGHL", "FGHP", "FGHZ", "FGLP", "FGLZ", "FGPZ", "FHJL", "FHJP", "FHJV", "FHJW", "FHLP", "FHLV", "FHLW", "FHLZ", "FHOV", "FHPV", "FHPW", "FHPZ", "FHQU", "FHQV", "FHQY", "FHRV", "FHSV", "FHUV", "FHUY", "FHVW", "FHVX", "FHVY", "FHVZ", "FJLP", "FJLV", "FJLW", "FJPV", "FJPW", "FJVW", "FLPV", "FLPW", "FLPZ", "FLVW", "FPVW", "FQUV", "FQUY", "FQVY", "FUVY", "GHLP", "GHLZ", "GHPZ", "GHVY", "GLPZ", "HJKV", "HJKY", "HJLP", "HJLQ", "HJLR", "HJLV", "HJLW", "HJLY", "HJLZ", "HJPV", "HJPW", "HJQY", "HJQZ", "HJVW", "HJVX", "HJVY", "HJXY", "HJYZ", "HKQX", "HKVX", "HKVY", "HKXY", "HLOZ", "HLPV", "HLPW", "HLPZ", "HLQY", "HLQZ", "HLRZ", "HLSZ", "HLVW", "HLXZ", "HLYZ", "HOQY", "HOVY", "HPQY", "HPVW", "HPVX", "HPVY", "HQSY", "HQUV", "HQUY", "HQVY", "HQXY", "HQYZ", "HSVY", "HUVY", "HVXY", "HVYZ", "HXYZ", "JLPV", "JLPW", "JLQY", "JLQZ", "JLVW", "JLYZ", "JPVW", "JQYZ", "JVXY", "LPVW", "LQYZ", "QUVY" };
    string fullPath = "";
    string validLetters;
    bool[][] signalMoments = {
        new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false },
        new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false },
        new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false },
        new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false }
    };
    string[] detected = { " ", " ", " ", " " };
    bool[] bulbed = { false, false, false, false };
    string invalidReasoning = "";
    KeyCode[] Keys = { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z };
    KeyCode[] AssistKeys = { KeyCode.Q, KeyCode.W, KeyCode.O, KeyCode.P };
    int currentMoment = 0;
    int finalMoment = 0;
    bool[] heldKeys = { false, false, false, false };

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;

        if (Application.isEditor) { focused = true; }
        ModuleSelectable.OnFocus += delegate () { focused = true; };
        ModuleSelectable.OnDefocus += delegate () { focused = false; };

        LEDSel.OnInteract += delegate () { LEDPress(); return false; };
        Submit.OnInteract += delegate () { SubmitPress(); return false; };
        
        foreach (KMSelectable Button in Buttons) {
            Button.OnInteract += delegate () { ButtonPress(Button); return false; };
        }

        foreach (KMSelectable Moment in LaneMomentSels) {
            Moment.OnInteract += delegate () { holding = true; MomentToggle(Moment); return false; };
            Moment.OnHighlight += delegate () { if (holding) { MomentToggle(Moment); } };
            Moment.OnInteractEnded += delegate () { holding = false; };
        }
    }

    // Use this for initialization
    void Start () {
        tryAgain:
        bool[] set = { true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        set = set.Shuffle();
        string letters = "";
        for (int e = 0; e < 26; e++) {
            if (set[e]) { letters += bet[e]; }
            if (letters.Length == 4) { break; }
        }
        if (impossiblePuzzles.Contains(letters)) {
            goto tryAgain;
        }
        validLetters = new string(letters.ToArray().Shuffle());

        string chosenOrder = orders.PickRandom();
        string[] fourPaths = { "", "", "", "" };
        for (int s = 0; s < 4; s++) {
            fourPaths[s] = RotatePath(paths[bet.IndexOf(validLetters[s])], s);
        }
        for (int d = 0; d < 4; d++) {
            fullPath += fourPaths[cw.IndexOf(chosenOrder[d])] + ((d != 3) ? InvertPath(fourPaths[cw.IndexOf(chosenOrder[d])]) : "");
        }
        Debug.LogFormat("[Worse Code #{0}] The LED sequence is as follows: {1}", moduleId, fullPath);
        Debug.LogFormat("[Worse Code #{0}] The path the observer took is as follows:\n{1}", moduleId, PrettifyPath());
        Debug.LogFormat("[Worse Code #{0}] The valid letters from that path are {1}", moduleId, validLetters);

        StartCoroutine(LEDPath(fullPath));
    }

    void Update() {
        if (!focused || moduleSolved) { return; }
        if (!submissionPhase) {
            for (int d = 0; d < 4; d++) {
                if (!assistMode) {
                    if (bet.Contains(detected[d])) {
                        if (Input.GetKeyDown(Keys[bet.IndexOf(detected[d])])) {
                            Audio.PlaySoundAtTransform("HOLD", transform);
                            heldKeys[d] = true;
                            ButtonLabels[d].color = Colors[3];
                        } else if (Input.GetKeyUp(Keys[bet.IndexOf(detected[d])])) {
                            Audio.PlaySoundAtTransform("RELEASE", transform);
                            heldKeys[d] = false;
                            ButtonLabels[d].color = Color.white;
                        }
                    }
                } else {
                    if (Input.GetKeyDown(AssistKeys[d])) {
                        Audio.PlaySoundAtTransform("HOLD", transform);
                        heldKeys[d] = true;
                        ButtonLabels[d].color = Colors[3];
                    } else if (Input.GetKeyUp(AssistKeys[d])) {
                        Audio.PlaySoundAtTransform("RELEASE", transform);
                        heldKeys[d] = false;
                        ButtonLabels[d].color = Color.white;
                    }
                }
            }
        } else {
            if (!assistMode) {
                for (int k = 0; k < Keys.Count(); k++) {
                    if (Input.GetKeyDown(Keys[k])) {
                        Audio.PlaySoundAtTransform("HOLD", transform);
                        if (validLetters.Contains(bet[k])) {
                            heldKeys[validLetters.IndexOf(bet[k])] = true;
                            if (KeyIsValid(validLetters.IndexOf(bet[k]), true)) {
                                CheckSolve();
                            } else {
                                Debug.LogFormat("[Worse Code #{0}] You held the {1} key at incorrect moment {2}, strike!", moduleId, bet[k], currentMoment+1);
                                StrikeStuff(validLetters.IndexOf(bet[k]));
                            }
                        } else {
                            Debug.LogFormat("[Worse Code #{0}] You pushed the {1} key which is not one of the letters, strike!", moduleId, bet[k]);
                            StrikeStuff(4);
                        }
                    } else if (Input.GetKeyUp(Keys[k])) {
                        Audio.PlaySoundAtTransform("RELEASE", transform);
                        if (validLetters.Contains(bet[k])) { //if this weren't here this gives an indexoutofrange i think
                            heldKeys[validLetters.IndexOf(bet[k])] = false;
                            if (KeyIsValid(validLetters.IndexOf(bet[k]), false)) {
                                CheckSolve();
                            } else {
                                Debug.LogFormat("[Worse Code #{0}] You released the {1} key at incorrect moment {2}, strike!", moduleId, bet[k], currentMoment+1);
                                StrikeStuff(validLetters.IndexOf(bet[k]));
                            }
                        }
                    }
                }
            } else {
                for (int a = 0; a < AssistKeys.Count(); a++) {
                    if (Input.GetKeyDown(AssistKeys[a])) {
                        Audio.PlaySoundAtTransform("HOLD", transform);
                        heldKeys[a] = true;
                        if (KeyIsValid(a, true)) {
                            CheckSolve();
                        } else {
                            Debug.LogFormat("[Worse Code #{0}] You held the {1} key at incorrect moment {2}, strike!", moduleId, ass[a], currentMoment+1);
                            StrikeStuff(a);
                        }
                    } else if (Input.GetKeyUp(AssistKeys[a])) {
                        Audio.PlaySoundAtTransform("RELEASE", transform);
                        heldKeys[a] = false;
                        if (KeyIsValid(a, false)) {
                            CheckSolve();
                        } else {
                            Debug.LogFormat("[Worse Code #{0}] You released the {1} key at incorrect moment {2}, strike!", moduleId, ass[a], currentMoment+1);
                            StrikeStuff(a);
                        }
                    }
                }
            }
        }
    }

    void StrikeStuff(int w) {
        GetComponent<KMBombModule>().HandleStrike();
        submissionPhase = false;
        for (int h = 0; h < 4; h++) {
            heldKeys[h] = false;
            ButtonLabels[h].color = Color.white;
        }
        for (int s = 0; s < 52; s++) {
            LaneMomentSprs[s].color = Color.white;
        }
        for (int p = 0; p < 4; p++) {
            if (w == p || w == 4) {
                StartCoroutine(AnimStrike(p));
            }
        }
        StartCoroutine(LEDPath(fullPath));
    }

    IEnumerator AnimStrike(int q) {
        ButtonLabels[q].color = Colors[0];
        yield return new WaitForSeconds(0.5f);
        ButtonLabels[q].color = Color.white;
    }

    void CheckSolve() {
        if (currentMoment == 13) {
            Audio.PlaySoundAtTransform("CLEAR", transform);
            Debug.LogFormat("[Worse Code #{0}] Final submission successful. Module solved.", moduleId);
            GetComponent<KMBombModule>().HandlePass();
            moduleSolved = true;
            for (int b = 0; b < 4; b++) {
                ButtonLabels[b].color = Colors[2];
            }
            LED.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            ButtonLabelObjs[4].transform.localScale = new Vector3(0.0012f, 0.0012f, 1f);
        }
    }

    IEnumerator LEDPath(string p) {
        while (true) {
            for (int m = 0; m < p.Length; m++) {
                StartCoroutine(RotateLED(cw.IndexOf(p[m])));
                yield return new WaitForSeconds(duration);
            }
            yield return new WaitForSeconds(duration*4);
        }
    }

    IEnumerator RotateLED(int d) {
        float elapsed = 0f;
        float initYZ = (d % 2) * 90f;
        while (elapsed < duration)
        {
            LED.transform.localRotation = Quaternion.Euler(90f + (elapsed/duration * ((d < 2) ? 360f : -360f)), initYZ, initYZ);
            yield return null;
            elapsed += Time.deltaTime;
        }
    }

    string RotatePath(string p, int r) {
        if (r == 0) { return p; }
        string syms = "#$&*";
        string lets = "urdl";
        string newp = p.Replace("u", "#").Replace("r", "$").Replace("d", "&").Replace("l", "*");
        for (int d = 0; d < 4; d++) {
            newp = newp.Replace(syms[d], lets[(d + r)%4]);
        }
        return newp;
    }

    string InvertPath(string p) {
        return new string(RotatePath(p, 2).Reverse().ToArray());
    }

    void SubmitPress() {
        if (submissionPhase || heldKeys[0] || heldKeys[1] || heldKeys[2] || heldKeys[3]) { return; }
        Audio.PlaySoundAtTransform("PRESS", transform);
        Submit.AddInteractionPunch();
        if (moduleSolved) { return; }
        Debug.LogFormat("[Worse Code #{0}] Submitting {1} ({2})", moduleId, MomentString(), detected.Join(""));
        if (MorseIsValid()) {
            Debug.LogFormat("[Worse Code #{0}] Morse code sequence detected as valid! Onto final submission...", moduleId);
            Debug.LogFormat("[Worse Code #{0}] Assist mode: {1}", moduleId, assistMode);
            submissionPhase = true;
            StopAllCoroutines();
            LED.transform.localRotation = Quaternion.Euler(270f, 0f, 180f);
            CalcMomentVars();
        } else {
            Debug.LogFormat("[Worse Code #{0}] Morse code sequence detected as invalid because {1}, strike!", moduleId, invalidReasoning);
            GetComponent<KMBombModule>().HandleStrike();
        }
    }
    
    void ButtonPress(KMSelectable Button) {
        if (submissionPhase) { return; }
        Audio.PlaySoundAtTransform("PRESS", transform);
        Button.AddInteractionPunch();
        if (moduleSolved) { return; }
        for (int b = 0; b < 4; b++) {
            if (Button == Buttons[b]) {
                if ("EIMOST".Contains(detected[b])) {
                    string beforeBulbasaur = detected[b];
                    bulbed[b] = !bulbed[b];
                    UpdateLane(b);
                    if (detected[b] == beforeBulbasaur) {
                        Debug.LogFormat("[Worse Code #{0}] Attempted to change letter '{1}' on lane {2} which is not possible given current Morse code sequence, strike!", moduleId, detected[b], b+1);
                        GetComponent<KMBombModule>().HandleStrike();
                    }
                } else {
                    Debug.LogFormat("[Worse Code #{0}] Attempted to change letter '{1}' on lane {2} which is impossible, strike!", moduleId, detected[b], b+1);
                    GetComponent<KMBombModule>().HandleStrike();
                }
            }
        }
    }

    void MomentToggle(KMSelectable Moment) {
        if (submissionPhase || moduleSolved) { return; }
        Audio.PlaySoundAtTransform("SELECT", transform);
        for (int m = 0; m < 52; m++) {
            if (Moment == LaneMomentSels[m]) {
                signalMoments[m/13][m%13] = !signalMoments[m/13][m%13];
                UpdateLane(m/13);
            }
        }
    }

    void UpdateLane(int l) {
        int signalCount = 0;
        List<int> sigLengths = new List<int> { };
        string sigString = "";

        for (int m = 0; m < 13; m++) {
            if (signalMoments[l][m]) {
                signalCount++;
                sigString += "=";
            } else {
                if (signalCount != 0) {
                    sigLengths.Add(signalCount);
                    signalCount = 0;
                }
                sigString += " ";
            }
        }
        if (signalCount != 0) {
            sigLengths.Add(signalCount);
        }

        int maxLen = -42;
        int minLen = 42;
        for (int s = 0; s < sigLengths.Count(); s++) {
            if (sigLengths[s] > maxLen) { maxLen = sigLengths[s]; }
            if (sigLengths[s] < minLen) { minLen = sigLengths[s]; }
        }

        string EQUALS = "=============";
        string UNDERS = "_____________";
        if (maxLen == minLen) {
            if (maxLen == 1) {
                bulbed[l] = false;
                sigString = sigString.Replace("=", "O");
            } else if (bulbed[l]) {
                if (sigString.Contains(EQUALS.Substring(0, maxLen))) {
                    sigString = sigString.Replace(EQUALS.Substring(0, maxLen), "(" + UNDERS.Substring(0, maxLen-2) + ")");
                }
            } else {
                //sigString = sigString.Replace(EQUALS.Substring(0, maxLen), "[" + UNDERS.Substring(0, maxLen-2) + "]");
            }
        } else {
            bulbed[l] = false;
            for (int e = 13; e > 0; e--) {
                if (e == 1) {
                    sigString = sigString.Replace("=", "O");
                } else if (e == maxLen) {
                    sigString = sigString.Replace(EQUALS.Substring(0, e), UNDERS.Substring(0, e));
                } else {
                    if (sigString.Contains(EQUALS.Substring(0, e))) {
                        sigString = sigString.Replace(EQUALS.Substring(0, e), "(" + UNDERS.Substring(0, e-2) + ")");
                    }
                }
            }
        }
        sigString = sigString.Replace("_", "=");

        for (int m = 0; m < 13; m++) {
            if (sigString[m] == ' ') {
                LaneMomentSprs[l*13 + m].sprite = null;
            } else {
                LaneMomentSprs[l*13 + m].sprite = Sprites["O=()".IndexOf(sigString[m])];
            }
        }

        string accumulatedMorse = "";
        for (int s = 0; s < sigLengths.Count(); s++) {
            if (sigLengths[s] == maxLen) {
                accumulatedMorse += (maxLen == 1 || bulbed[l]) ? "." : "-";
            } else if (sigLengths[s] == minLen) {
                accumulatedMorse += ".";
            } else {
                accumulatedMorse = "~";
                break;
            }
        }
        if (morseBet.Contains(accumulatedMorse)) {
            detected[l] = bet[Array.IndexOf(morseBet, accumulatedMorse)].ToString();
        } else {
            detected[l] = (accumulatedMorse == "") ? "" : "?";
        }
        ButtonLabels[l].text = assistMode ? detected[l].ToLower() : detected[l];
    }
    
    string MomentString() {
        string z = "";
        for (int y = 0; y < 52; y++) {
            z += signalMoments[y/13][y%13] ? "#" : "-";
            if (y % 13 == 12 && y != 51) { z += "/"; }
        }
        return z;
    }

    bool MorseIsValid() {
        string[] ms = MomentString().Split('/');
        for (int l = 0; l < 4; l++) {
            if (ms[l].IndexOf('#') % 2 != 0) {
                invalidReasoning = "a lane started at an even moment";
                return false;
            }
            ms[l] = ms[l].Trim('-');
        }
        for (int l = 0; l < 4; l++) {
            if (ms[l].Contains("--")) {
                invalidReasoning = "there is space between signal greater than 1 moment";
                return false;
            }
        }

        for (int l = 0; l < 4; l++) {
            int signalCountAgain = 0;
            List<int> sigLengthsAgain = new List<int> { };

            for (int m = 0; m < 13; m++) {
                if (signalMoments[l][m]) {
                    signalCountAgain++;
                } else {
                    if (signalCountAgain != 0) {
                        sigLengthsAgain.Add(signalCountAgain);
                        signalCountAgain = 0;
                    }
                }
            }
            if (signalCountAgain != 0) {
                sigLengthsAgain.Add(signalCountAgain);
            }

            for (int s = 0; s < sigLengthsAgain.Count(); s++) {
                if (sigLengthsAgain[s] % 2 != 1) {
                    invalidReasoning = "there exists consecutive signal of length that cannot be 2w+1";
                    return false;
                }
            }
        }

        if (detected.Join("") != validLetters) {
            invalidReasoning = "one or more of the letters are invalid or in the wrong order";
            return false;
        }
        string theExcitingPart = "";
        for (int m = 0; m < 13; m++) {
            theExcitingPart += (signalMoments[0][m] || signalMoments[1][m] || signalMoments[2][m] || signalMoments[3][m]) ? "#" : " ";
        }
        theExcitingPart = theExcitingPart.Trim(' ');
        if (theExcitingPart.Contains(" ")) {
            invalidReasoning = "columns containing signal are not consecutive";
            return false;
        }

        return true;
    }

    string PrettifyPath() {
        bool[][] prettyBools = {
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }
        };
        int[] pos = { 10, 10 };
        for (int v = 0; v < fullPath.Length; v++) {
            for (int a = 0; a < 2; a++) {
                switch (fullPath[v]) {
                    case 'u': pos[1]--; break;
                    case 'r': pos[0]++; break;
                    case 'd': pos[1]++; break;
                    case 'l': pos[0]--; break;
                }
                prettyBools[pos[0]][pos[1]] = true;
            }
        }
        string prettyPath = "";
        for (int x = 0; x < 21; x++) { //i know it makes like negative sense but swapping these will not work!
            for (int y = 0; y < 21; y++) {
                prettyPath += prettyBools[y][x] ? "█" : " ";
            }
            prettyPath += "\n";
        }
        return prettyPath;
    }

    void CalcMomentVars() {
        for (int m = 0; m < 13; m++) {
            if (signalMoments[0][m] || signalMoments[1][m] || signalMoments[2][m] || signalMoments[3][m]) {
                currentMoment = m;
                break;
            }
        }
        for (int m = 12; m > -1; m--) {
            if (signalMoments[0][m] || signalMoments[1][m] || signalMoments[2][m] || signalMoments[3][m]) {
                finalMoment = m;
                break;
            }
        }
    }

    bool KeyIsValid(int x, bool b) {
        if (signalMoments[x][currentMoment] == b) {
            LaneMomentSprs[x*13 + currentMoment].color = Colors[1];
            ButtonLabels[x].color = b ? Colors[3] : Color.white;
            while (heldKeys[0] == signalMoments[0][currentMoment] && heldKeys[1] == signalMoments[1][currentMoment] && heldKeys[2] == signalMoments[2][currentMoment] && heldKeys[3] == signalMoments[3][currentMoment]) {
                LaneMomentSprs[currentMoment].color = Colors[2];
                LaneMomentSprs[13 + currentMoment].color = Colors[2];
                LaneMomentSprs[26 + currentMoment].color = Colors[2];
                LaneMomentSprs[39 + currentMoment].color = Colors[2];
                currentMoment++;
                if (currentMoment == 13) {
                    break;
                }
            }
        } else {
            return false;
        }
        return true;
    }

    void LEDPress() {
        if (submissionPhase || moduleSolved) { return; }
        Audio.PlaySoundAtTransform("PRESS", transform);
        assistMode = !assistMode;
        if (assistMode) {
            for (int l = 0; l < 4; l++) {
                ButtonLabels[l].text = ButtonLabels[l].text.ToLower();
                ButtonLabelObjs[l].transform.localScale = new Vector3(0.0015f, 0.0015f, 1f);
            }
        } else {
            for (int l = 0; l < 4; l++) {
                ButtonLabels[l].text = ButtonLabels[l].text.ToUpper();
                ButtonLabelObjs[l].transform.localScale = new Vector3(0.002f, 0.002f, 1f);
            }
        }
        for (int a = 0; a < 4; a++) {
            heldKeys[a] = false;
            ButtonLabels[a].color = Color.white;
        }
    }
}

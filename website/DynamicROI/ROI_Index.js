//const segments = [
//    { term: "1 Month", rate: "3.20%" },
//    { term: "2 Months", rate: "3.35%" },
//    { term: "3 Months", rate: "3.50%" },
//    { term: "6 Months", rate: "3.60%" },
//    { term: "9 Months", rate: "3.70%" },
//    { term: "1 Year", rate: "4.23%" },
//    // { term: "18 Months", rate: "4.10%" },
//    // { term: "2 Years",   rate: "4.00%" },
//    //{ term: "3 Years",   rate: "3.75%" },
//    // { term: "2 Years",   rate: "4.00%" },
//    // { term: "3 Years",   rate: "3.75%" },
//    // { term: "2 Years",   rate: "4.00%" },
//];

const SVG_NS = "http://www.w3.org/2000/svg";

console.log("segments:", segments);
const svg = document.getElementById("rateWheel");
console.log("svg:", svg);

if (!svg) {
    console.error("SVG not found");
}


const CX = 210, CY = 210;

const R_OUTER_CARD = 205;
const R_CARD_INNER = 128;
const R_SPOKE_OUT = 126;
const R_SPOKE_IN = 58;
const R_CENTER = 55;

const N = segments.length;
const SLICE = (2 * Math.PI) / N;
const GAP = (2.5 * Math.PI) / 180;

const COL_NAVY = "#0a63a5";
const COL_RED = "#cc1e28";
const COL_WHITE = "#FFFFFF";

const polar = (cx, cy, r, a) => ({ x: cx + r * Math.cos(a), y: cy + r * Math.sin(a) });

function el(tag, attrs = {}, parent) {
    const e = document.createElementNS(SVG_NS, tag);
    for (const [k, v] of Object.entries(attrs)) e.setAttribute(k, v);
    if (parent) parent.appendChild(e);
    return e;
}

function mkText(content, attrs = {}, parent) {
    const t = document.createElementNS(SVG_NS, "text");
    for (const [k, v] of Object.entries(attrs)) t.setAttribute(k, v);
    t.textContent = content;
    if (parent) parent.appendChild(t);
    return t;
}

// ── ROTATABLE RING GROUP ──
const wheelRing = el("g", { id: "wheelRing" }, svg);

// ── OUTER WHITE PETALS ──
segments.forEach((seg, i) => {
    const mid = -Math.PI / 2 + i * SLICE + SLICE / 2;
    const start = -Math.PI / 2 + i * SLICE + GAP / 2;
    const end = start + SLICE - GAP;

    const p1 = polar(CX, CY, R_CARD_INNER, start);
    const p2 = polar(CX, CY, R_CARD_INNER, end);
    const p3 = polar(CX, CY, R_OUTER_CARD, end);
    const p4 = polar(CX, CY, R_OUTER_CARD, start);

    const d = `M ${p1.x} ${p1.y} A ${R_CARD_INNER} ${R_CARD_INNER} 0 0 1 ${p2.x} ${p2.y} L ${p3.x} ${p3.y} A ${R_OUTER_CARD} ${R_OUTER_CARD} 0 0 0 ${p4.x} ${p4.y} Z`;
    const g = el("g", { filter: "url(#cardShadow)" }, wheelRing);
    el("path", { d, fill: COL_WHITE }, g);
});

// ── INNER SPOKE RING ──
segments.forEach((seg, i) => {
    const start = -Math.PI / 2 + i * SLICE + GAP / 2;
    const end = start + SLICE - GAP;

    const p1 = polar(CX, CY, R_SPOKE_IN, start);
    const p2 = polar(CX, CY, R_SPOKE_IN, end);
    const p3 = polar(CX, CY, R_SPOKE_OUT, end);
    const p4 = polar(CX, CY, R_SPOKE_OUT, start);

    const d = `M ${p1.x} ${p1.y} A ${R_SPOKE_IN} ${R_SPOKE_IN} 0 0 1 ${p2.x} ${p2.y} L ${p3.x} ${p3.y} A ${R_SPOKE_OUT} ${R_SPOKE_OUT} 0 0 0 ${p4.x} ${p4.y} Z`;
    el("path", { d, fill: i % 2 === 0 ? COL_NAVY : COL_RED }, wheelRing);
});

// ── LABELS ──
segments.forEach((seg, i) => {
    const mid = -Math.PI / 2 + i * SLICE + SLICE / 2;
    const rateR = (R_CARD_INNER + R_OUTER_CARD) / 2 + 8;
    const spokeR = (R_SPOKE_IN + R_SPOKE_OUT) / 2;  // center of spoke band

    const rp = polar(CX, CY, rateR, mid);
    const sp = polar(CX, CY, spokeR, mid);

    const normMid = ((mid * 180 / Math.PI) % 360 + 360) % 360;
    const flip = (normMid > 0 && normMid < 180) ? 180 : 0;
    const deg = (mid * 180) / Math.PI + 90 + flip;

    // ── Rate label (single line) ──
    const rg = el("g", { transform: `translate(${rp.x},${rp.y}) rotate(${deg})` }, wheelRing);
    mkText(seg.rate, {
        x: "0", y: "0",
        "text-anchor": "middle", "dominant-baseline": "central",
        //"font-family": "Montserrat,sans-serif",
        "font-size": "16", "font-weight": "600",
        fill: i % 2 === 0 ? COL_RED : COL_NAVY
    }, rg);

    // ── Term label — split into number line + unit line ──
    const parts = seg.term.split(" ");           // ["1","Month"] or ["18","Months"]
    const numPart = parts[0];                   // "1", "18", "3" …
    const unitPart = parts.slice(1).join(" ");   // "Month", "Months", "Year", "Years"

    const lineH = 7;   // half-gap between the two lines
    const sg = el("g", { transform: `translate(${sp.x},${sp.y}) rotate(${deg})` }, wheelRing);

    mkText(numPart, {
        x: "0", y: -lineH,
        "text-anchor": "middle", "dominant-baseline": "central",
        //"font-family": "Montserrat,sans-serif",
        "font-size": "12", "font-weight": "600",
        fill: COL_WHITE
    }, sg);

    mkText(unitPart, {
        x: "0", y: lineH,
        "text-anchor": "middle", "dominant-baseline": "central",
        //"font-family": "Montserrat,sans-serif",
        "font-size": "10", "font-weight": "600",
        fill: COL_WHITE
    }, sg);
});

// ── ARROWS ──
segments.forEach((_, i) => {
    const mid = -Math.PI / 2 + i * SLICE + SLICE / 2;
    const tipR = R_CARD_INNER + 10;
    const baseR = R_SPOKE_OUT - 8;

    const tip = polar(CX, CY, tipR, mid);
    const bl = polar(CX, CY, baseR, mid - 0.16);
    const br = polar(CX, CY, baseR, mid + 0.16);

    el("polygon", {
        points: `${tip.x},${tip.y} ${bl.x},${bl.y} ${br.x},${br.y}`,
        fill: i % 2 === 0 ? COL_NAVY : COL_RED
    }, wheelRing);
});

// ── CENTRE CIRCLE ──
el("circle", { cx: CX, cy: CY, r: R_CENTER + 4, fill: COL_WHITE, filter: "url(#centerGlow)" }, svg);
el("image", {
    href: "website/DynamicROI/ubi_center.png",
    x: CX - (R_CENTER - 2),
    y: CY - (R_CENTER - 2),
    width: (R_CENTER - 2) * 2,
    height: (R_CENTER - 2) * 2,
    preserveAspectRatio: "xMidYMid meet",
    "clip-path": "url(#centerClip)"
}, svg);
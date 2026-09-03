
function DrawROI() {

    //// ════════════════════════════════════════
    ////  ✏  EDIT SEGMENTS HERE (min 5, max 12)
    //// ════════════════════════════════════════
    //const segs = [
    //    // { rate:"3.50%", term:"3 Months", col:"#e51e1e" },
    //    //{ rate:"3.60%", term:"6 Months", col:"#0b5ea8" },
    //    //{ rate:"4.45%", term:"9 Months", col:"#e51e1e" },
    //    { rate: "4.75%", term: "1 Year", col: "#0b5ea8" },
    //    //{ rate:"4.60%", term:"15 Months",  col:"#e51e1e" },
    //    // { rate:"4.50%", term:"18 Months",  col:"#0b5ea8" },
    //    { rate: "4.25%", term: "2 Years", col: "#e51e1e" },
    //    { rate: "3.90%", term: "3 Years", col: "#0b5ea8" },
    //    { rate: "3.85%", term: "4 Years", col: "#e51e1e" },
    //    { rate: "3.75%", term: "5 Years", col: "#0b5ea8" },
    //    // { rate:"3.25%", term:"6 Years",  col:"#e51e1e" },
    //    //{ rate:"3.00%", term:"7 Years",  col:"#0b5ea8" },
    //];
    //// ════════════════════════════════════════

    const SVG_NS = "http://www.w3.org/2000/svg";
    const svg = document.getElementById("svg");
    const CX = 350, CY = 350;
    const N = Math.min(12, Math.max(5, segs.length));

    function lerp(a, b, t) { return a + (b - a) * t; }
    const t = (N - 5) / 7;

    const ORBIT = lerp(210, 230, t);
    const NODE_R = lerp(60, 45, t);
    const WHITE_R = NODE_R + lerp(0, 0, t);
    const RING_IN = lerp(130, 170, t);
    const PINCH_DEP = lerp(0, 0, t);
    const FONT_RATE = lerp(26, 20, t);
    const FONT_TERM = lerp(14, 14, t);
    const TENSION = lerp(0.32, 0.26, t);

    function polar(r, a) { return [CX + r * Math.cos(a), CY + r * Math.sin(a)]; }
    function angOf(i) { return -Math.PI / 2 + i * (2 * Math.PI / N); }

    const centers = Array.from({ length: N }, (_, i) => {
        const a = angOf(i);
        return { x: polar(ORBIT, a)[0], y: polar(ORBIT, a)[1], a };
    });

    function el(tag, attrs = {}, parent = svg) {
        const e = document.createElementNS(SVG_NS, tag);
        for (const [k, v] of Object.entries(attrs)) e.setAttribute(k, v);
        parent.appendChild(e);
        return e;
    }

    // ── White lobed ring ──
    let pts = [];
    for (let i = 0; i < N; i++) {
        pts.push({ r: ORBIT + WHITE_R * 0.85, a: angOf(i) });
        pts.push({ r: ORBIT - PINCH_DEP, a: angOf(i) + Math.PI / N });
    }

    function smoothPath(pts) {
        const n = pts.length;
        const c = pts.map(p => [CX + p.r * Math.cos(p.a), CY + p.r * Math.sin(p.a)]);
        let d = `M${c[0][0]},${c[0][1]} `;
        for (let i = 0; i < n; i++) {
            const p0 = c[(i - 1 + n) % n], p1 = c[i], p2 = c[(i + 1) % n], p3 = c[(i + 2) % n];
            const cp1x = p1[0] + (p2[0] - p0[0]) * TENSION, cp1y = p1[1] + (p2[1] - p0[1]) * TENSION;
            const cp2x = p2[0] - (p3[0] - p1[0]) * TENSION, cp2y = p2[1] - (p3[1] - p1[1]) * TENSION;
            d += `C${cp1x},${cp1y} ${cp2x},${cp2y} ${p2[0]},${p2[1]} `;
        }
        return d + 'Z';
    }

    const outerD = smoothPath(pts);
    const r = RING_IN;
    const innerD = `M${CX + r},${CY} A${r},${r} 0 1 0 ${CX - r},${CY} A${r},${r} 0 1 0 ${CX + r},${CY} Z`;

    el("path", { d: outerD + ' ' + innerD, fill: "white", "fill-rule": "evenodd", filter: "url(#ws)" });

    // ── Center circle ──
    //el("circle",{ cx:CX, cy:CY, r:RING_IN+8, fill:"url(#cg)", filter:"url(#cs)" });

    // ── Nodes ──
    centers.forEach((c, i) => {
        const seg = segs[i];
        el("circle", { cx: c.x, cy: c.y, r: NODE_R + 10, fill: "white" });
        el("circle", { cx: c.x, cy: c.y, r: NODE_R, fill: seg.col, filter: "url(#ns)" });

        const rt = el("text", {
            x: c.x, y: c.y - FONT_RATE * 0.45,
            "text-anchor": "middle", "dominant-baseline": "central",
            /*"font-family": "Montserrat,sans-serif",*/
            "font-size": FONT_RATE, "font-weight": "600", "fill": "white"
        });
        rt.textContent = seg.rate;

        const tt = el("text", {
            x: c.x, y: c.y + FONT_TERM * 0.75,
            "text-anchor": "middle", "dominant-baseline": "central",
            //"font-family": "Montserrat,sans-serif",
            "font-size": FONT_TERM, "font-weight": "600", "fill": "white"
        });
        tt.textContent = seg.term;
    });
}

Sys.Application.add_load(function () {

    DrawROI();

});

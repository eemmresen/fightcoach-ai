---
name: Apex Striker
colors:
  surface: '#0b1326'
  surface-dim: '#0b1326'
  surface-bright: '#31394d'
  surface-container-lowest: '#060e20'
  surface-container-low: '#131b2e'
  surface-container: '#171f33'
  surface-container-high: '#222a3d'
  surface-container-highest: '#2d3449'
  on-surface: '#dae2fd'
  on-surface-variant: '#c2c7d0'
  inverse-surface: '#dae2fd'
  inverse-on-surface: '#283044'
  outline: '#8c9199'
  outline-variant: '#42474f'
  surface-tint: '#a0cafc'
  primary: '#a0cafc'
  on-primary: '#003257'
  primary-container: '#1f4e79'
  on-primary-container: '#95bff1'
  inverse-primary: '#35618d'
  secondary: '#9ecaff'
  on-secondary: '#003258'
  secondary-container: '#005c9c'
  on-secondary-container: '#b2d4ff'
  tertiary: '#ffb3ae'
  on-tertiary: '#640b11'
  tertiary-container: '#8a2828'
  on-tertiary-container: '#ffa39e'
  error: '#ffb4ab'
  on-error: '#690005'
  error-container: '#93000a'
  on-error-container: '#ffdad6'
  primary-fixed: '#d1e4ff'
  primary-fixed-dim: '#a0cafc'
  on-primary-fixed: '#001d35'
  on-primary-fixed-variant: '#184974'
  secondary-fixed: '#d1e4ff'
  secondary-fixed-dim: '#9ecaff'
  on-secondary-fixed: '#001d36'
  on-secondary-fixed-variant: '#00497d'
  tertiary-fixed: '#ffdad7'
  tertiary-fixed-dim: '#ffb3ae'
  on-tertiary-fixed: '#410005'
  on-tertiary-fixed-variant: '#842324'
  background: '#0b1326'
  on-background: '#dae2fd'
  surface-variant: '#2d3449'
  success-green: '#28a745'
  warning-yellow: '#ffc107'
  surface-slate: '#1e293b'
  border-muted: '#334155'
  text-primary: '#f8fafc'
  text-secondary: '#94a3b8'
typography:
  display-lg:
    fontFamily: Montserrat
    fontSize: 72px
    fontWeight: '900'
    lineHeight: '1.1'
    letterSpacing: -0.02em
  display-lg-mobile:
    fontFamily: Montserrat
    fontSize: 40px
    fontWeight: '900'
    lineHeight: '1.1'
  headline-xl:
    fontFamily: Montserrat
    fontSize: 32px
    fontWeight: '700'
    lineHeight: '1.2'
  headline-lg:
    fontFamily: Montserrat
    fontSize: 24px
    fontWeight: '700'
    lineHeight: '1.3'
  headline-md:
    fontFamily: Montserrat
    fontSize: 20px
    fontWeight: '600'
    lineHeight: '1.4'
  body-lg:
    fontFamily: Inter
    fontSize: 18px
    fontWeight: '400'
    lineHeight: '1.6'
  body-md:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '400'
    lineHeight: '1.6'
  label-md:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '600'
    lineHeight: '1.2'
    letterSpacing: 0.05em
  data-num:
    fontFamily: Montserrat
    fontSize: 28px
    fontWeight: '800'
    lineHeight: '1'
    letterSpacing: -0.01em
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  container-max: 1200px
  gutter: 1.5rem
  margin-mobile: 1rem
  stack-sm: 0.5rem
  stack-md: 1.5rem
  stack-lg: 3rem
---

## Brand & Style

The design system embodies a **"Strava for Fighters"** aesthetic, blending high-performance sports science with clinical data precision. It is designed to evoke a sense of professional authority, discipline, and relentless progress. The brand personality is technical and elite, positioning the user not just as an athlete, but as a data-driven technician of combat.

The visual style is a fusion of **Corporate Modern** and **High-Contrast Data-Driven** aesthetics. It utilizes a deep dark-mode foundation to mimic the focused environment of a high-end MMA gym or a performance lab. Key characteristics include:
- **Precision Grids:** Strict adherence to alignment to convey technical accuracy.
- **Data Density:** High information density handled through clear hierarchy and "glanceable" metrics.
- **Dynamic Vitality:** Sharp accents of "Impact Red" and "Athletic Blue" to inject energy into the dark, analytical interface.
- **Layered Technicality:** Use of semi-transparent surfaces and subtle borders to create a sophisticated, multi-dimensional dashboard feel.

## Colors

The palette is anchored in a **Dark Mode** default to prioritize contrast and reduce eye strain in intense environments. 

- **Primary (Deep Navy):** Used for structural authority, primary navigation backgrounds, and deep grounding elements.
- **Secondary (Athletic Blue):** The active brand color. Used for interactive elements, progress indicators, and primary actions.
- **Tertiary (Impact Red):** Reserved for high-energy accents, critical alerts, and "danger zone" performance metrics.
- **Neutral (Deep Slate/Navy):** The foundation of the UI, using varying shades of navy-tinted grays to maintain a premium, cohesive atmosphere.
- **Semantic Scoring:** Performance metrics follow a strict color scale:
    - **Elite (90-100):** Primary/Deep Navy
    - **Very Good (75-89):** Success Green
    - **Average (40-74):** Warning Yellow/Orange
    - **Critical (0-39):** Impact Red

## Typography

This design system uses a dual-font strategy to balance aggressive branding with technical legibility.

- **Montserrat (Headlines/Data):** Used for all high-impact headers and numerical data points. The heavy weights (700-900) should be used for scores and titles to project strength.
- **Inter (Body/UI):** Used for all functional text, descriptions, and labels. Its neutral, systematic nature ensures that complex data tables remain readable.

**Styling Notes:**
- **Numerical Data:** Always use `data-num` (Montserrat) for performance scores to differentiate them from standard text.
- **Case Styling:** Use UPPERCASE for `label-md` to create a "tactical" HUD feel.
- **Letter Spacing:** Tighten spacing for large headlines and slightly increase it for small labels to improve clarity.

## Layout & Spacing

The layout utilizes a **12-column fluid grid** for dashboard views and a **centered fixed-width container (max-width 1200px)** for content-heavy reports.

- **Rhythm:** A base-8 scale is used for all spacing. Consistent vertical rhythm is maintained via "stack" variables.
- **Grid-Based Metrics:** Performance data should be presented in modular grids (2, 3, or 4 columns) that reflow into single columns on mobile.
- **Safe Areas:** On mobile devices, side margins are reduced to 16px to maximize the real estate for complex charts.
- **Section Breaks:** Large 3rem (48px) gaps separate major analytical modules, ensuring the UI doesn't feel cluttered despite high data density.

## Elevation & Depth

Hierarchy is established through **Tonal Layering** and **Backdrop Blurs** rather than traditional heavy shadows. This maintains a sleek, digital "glass" aesthetic.

- **Surface Levels:** 
  - **Level 0 (Background):** `#0f172a` (Base).
  - **Level 1 (Cards/Sections):** `#1e293b` (Elevated surface).
  - **Level 2 (Modals/Popovers):** `#334155` (Highest contrast).
- **Glassmorphism:** Navigation bars and sticky headers must use a 10px-15px backdrop blur with a 70% opaque primary color background.
- **Tactical Outlines:** Instead of shadows, use 1px solid borders in `border-muted` or 2px left-accent borders in `secondary_color` to define card boundaries.
- **Micro-Shadows:** Use a single, very subtle shadow for floating action buttons: `0 4px 12px rgba(0, 0, 0, 0.4)`.

## Shapes

The shape language is **"Modern Technical."** It avoids extreme roundness to maintain a serious, athletic tone, opting instead for consistent, medium radii.

- **Standard Elements:** Buttons, cards, and input fields use a `0.5rem (8px)` radius.
- **Large Components:** Dashboard sections and main containers use `1rem (16px)` to soften the "industrial" feel slightly.
- **Performance Rings:** Visualizations like progress circles and "Fight IQ" charts use geometric circularity (100% radius) to contrast against the rectangular grid.
- **Accents:** Use sharp-edged, 2px-4px vertical "accent bars" on the left side of active cards to denote focus.

## Components

### Buttons
- **Primary:** Solid `secondary_color` with white text. High-contrast, bold weight.
- **Secondary:** Outlined with `secondary_color`, 2px border width.
- **Ghost:** No background, `text-secondary` color, shifting to `text-primary` on hover.

### Cards
- **Dashboard Cards:** Use `Level 1` surface color. 1px `border-muted` stroke. Header areas should have a subtle bottom divider.
- **Metric Cards:** Feature a large Montserrat score (`data-num`) centered or top-aligned, with a `label-md` descriptive text below.

### Progress & Visualization
- **Progress Rings:** Use thick stroke widths (8px+). Use the semantic color scale for the ring's stroke based on the percentage value.
- **Radar Charts:** Semi-transparent fills of `secondary_color` with high-contrast white grid lines.

### Inputs & Selection
- **Input Fields:** Darker background than the card surface. 1px focus ring in `secondary_color`.
- **Chips/Badges:** Small, high-contrast pills. Use `Impact Red` for "Critical" or "High Intensity" tags.

### Lists & Tables
- **Data Tables:** Zebra stripping using a slightly lighter navy tint on even rows. No vertical borders; only subtle horizontal dividers.
- **List Items:** Use the `▸` character as a custom bullet point, colored in `secondary_color` for a technical, directed look.
﻿using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Linq;

namespace SharpBgfx {
    [SuppressUnmanagedCodeSecurity]
    public unsafe static class Bgfx {
        const string DllName = "bgfx.dll";

        [DllImport(DllName, EntryPoint = "bgfx_get_renderer_type", CallingConvention = CallingConvention.Cdecl)]
        public static extern RendererType GetCurrentRenderer ();

        [DllImport(DllName, EntryPoint = "bgfx_init", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Init (RendererType rendererType, IntPtr p, IntPtr d);

        [DllImport(DllName, EntryPoint = "bgfx_shutdown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Shutdown ();

        [DllImport(DllName, EntryPoint = "bgfx_reset", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Reset (int width, int height, ResetFlags flags);

        [DllImport(DllName, EntryPoint = "bgfx_set_debug", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDebugFlags (DebugFlags flags);

        [DllImport(DllName, EntryPoint = "bgfx_set_marker", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDebugMarker (string marker);

        [DllImport(DllName, EntryPoint = "bgfx_frame", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Frame ();

        [DllImport(DllName, EntryPoint = "bgfx_get_caps", CallingConvention = CallingConvention.Cdecl)]
        public static extern Caps* GetCaps ();

        [DllImport(DllName, EntryPoint = "bgfx_win_set_hwnd", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetWindowHandle (IntPtr hwnd);

        [DllImport(DllName, EntryPoint = "bgfx_get_renderer_name", CallingConvention = CallingConvention.Cdecl)]
        static extern sbyte* GetRendererNameInternal (RendererType rendererType);

        public static string GetRendererName (RendererType rendererType) {
            return new string(GetRendererNameInternal(rendererType));
        }

        [DllImport(DllName, EntryPoint = "bgfx_get_supported_renderers", CallingConvention = CallingConvention.Cdecl)]
        static extern byte GetSupportedRenderers (RendererType[] types);

        public static RendererType[] GetSupportedRenderers () {
            var types = new RendererType[Enum.GetValues(typeof(RendererType)).Length];
            var count = GetSupportedRenderers(types);

            return types.Take(count).ToArray();
        }

        [DllImport(DllName, EntryPoint = "bgfx_vertex_decl_begin", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VertexDeclBegin (ref VertexDecl decl, RendererType renderer);

        [DllImport(DllName, EntryPoint = "bgfx_vertex_decl_add", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VertexDeclAdd (ref VertexDecl decl, VertexAttribute attribute, byte count, VertexAttributeType type, bool normalized, bool asInt);

        [DllImport(DllName, EntryPoint = "bgfx_vertex_decl_skip", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VertexDeclSkip (ref VertexDecl decl, byte count);

        [DllImport(DllName, EntryPoint = "bgfx_vertex_decl_end", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VertexDeclEnd (ref VertexDecl decl);

        [DllImport(DllName, EntryPoint = "bgfx_alloc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Alloc (int size);

        [DllImport(DllName, EntryPoint = "bgfx_copy", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Copy (IntPtr data, int size);

        [DllImport(DllName, EntryPoint = "bgfx_make_ref", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MakeRef (IntPtr data, int size);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_name", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewName (byte id, string name);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_rect", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewRect (byte id, ushort x, ushort y, ushort width, ushort height);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_rect_mask", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewRectMask (int viewMask, ushort x, ushort y, ushort width, ushort height);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_scissor", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewScissor (byte id, ushort x, ushort y, ushort width, ushort height);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_scissor_mask", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewScissorMask (int viewMask, ushort x, ushort y, ushort width, ushort height);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_clear", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewClear (byte id, ClearFlags flags, int rgba, float depth, byte stencil);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_clear_mask", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewClearMask (int viewMask, ClearFlags flags, int rgba, float depth, byte stencil);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_seq", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewSequential (byte id, bool enabled);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_seq_mask", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewSequentialMask (int viewMask, bool enabled);

        [DllImport(DllName, EntryPoint = "bgfx_submit", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Submit (byte id, int depth);

        [DllImport(DllName, EntryPoint = "bgfx_submit_mask", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SubmitMask (int viewMask, int depth);

        [DllImport(DllName, EntryPoint = "bgfx_discard", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Discard ();

        [DllImport(DllName, EntryPoint = "bgfx_save_screen_shot", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SaveScreenShot (string filePath);
    }
}
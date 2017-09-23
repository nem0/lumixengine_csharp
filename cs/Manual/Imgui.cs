using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


public class ImGui
{
    [MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void AlignFirstTextHeightToWidgets();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool BeginChildFrame(uint id, Lumix.Vec2 size, int flags);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool BeginDock(string label, ref bool open, int extra_fags, Lumix.Vec2 size);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool BeginPopup(string id);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool Button(string label, Lumix.Vec2 size);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool Checkbox(string label, ref bool is_checked);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void Columns(int count, string id, bool border);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool DragFloat(string label, ref float v, float v_speed, float v_max, float v_min, string display_format, float power);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void Dummy(Lumix.Vec2 size);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void End();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void EndChildFrame();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void EndDock();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void EndPopup();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static float GetColumnWidth(int column_index);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static float GetWindowWidth();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static float GetWindowHeight();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static Lumix.Vec2 GetWindowSize();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void Indent(float w);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool IsItemHovered();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool InputInt(string label, ref int v, int step, int step_fast, int extra_flags);

	public unsafe static bool InputText(string label, byte[] textBuffer, int flags, IntPtr textEditCallback, IntPtr userData)
	{
		fixed (byte* ptrBuf = textBuffer)
		{
			return InputText(label, new IntPtr(ptrBuf), (uint)textBuffer.Length, flags, textEditCallback, userData);
		}
	}

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool InputText(string label, IntPtr buf, uint buf_size, int flags, IntPtr callback, IntPtr user_data);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool IsMouseClicked(int button, bool repeat);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool IsMouseDown(int button);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void NewLine();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void NextColumn();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void OpenPopup(string id);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void PopItemWidth();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void PopID();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void PopStyleColor();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void PushItemWidth(float w);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void PushStyleColor(int idx, Lumix.Vec4 color);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void Rect(float w, float h, UInt32 color);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void SameLine(float pos_x, float spacing);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]

	public extern static bool Selectable(string label, bool selected, int flags, Lumix.Vec2 size_arg);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void Separator();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void SetCursorScreenPos(Lumix.Vec2 pos);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void SetNextWindowPos(Lumix.Vec2 pos, int cond);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void SetNextWindowPosCenter(int cond);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void SetNextWindowSize(Lumix.Vec2 pos, int cond);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void ShowTestWindow(ref bool open);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static bool SliderFloat(string label, ref float v, float v_min, float v_max, string display_format, float power);

    [MethodImplAttribute(MethodImplOptions.InternalCall)]
    public extern static void Text(string val);

    [MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void Unindent(float w);
}

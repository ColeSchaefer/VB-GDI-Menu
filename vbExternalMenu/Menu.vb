Imports System.Drawing.Drawing2D

Public Class Menu
    Public MenuTitle As String = "VB.net GDI Menu"
    Public MenuWidth As Integer = 250
    Public MenuFont As Font = New Font("Verdana", 8.25F, FontStyle.Regular)

    Public Const ITEM_HEIGHT As Integer = 25
    Public Const HEADER_HEIGHT As Integer = 50

    Private SelectedIndex As Integer = 0
    Private HeaderColor As Color = Color.DodgerBlue
    Private MenuMoving As Boolean = False
    Private MouseP As Point = Nothing
    Private MenuItems As List(Of String) = Nothing

    Public Sub PopulateMenuItems()
        MenuItems = New List(Of String)
        MenuItems.Add("Menu Item 1")
        MenuItems.Add("Menu Item 2")
        MenuItems.Add("Menu Item 3")
        MenuItems.Add("Menu Item 4")
        MenuItems.Add("Menu Item 5")
        MenuItems.Add("Menu Item 6")
        MenuItems.Add("Menu Item 7")
        MenuItems.Add("Menu Item 8")
        MenuItems.Add("Menu Item 9")
        MenuItems.Add("Menu Item 10")
    End Sub

    Public Sub HandleMenuClick()
        Console.WriteLine(String.Format("Selected Menu Item: {0} ({1})", MenuItems(SelectedIndex), SelectedIndex))
        Select Case SelectedIndex
            Case 0
                MessageBox.Show("You selected item #1!")
            Case 1
                MessageBox.Show("You selected item #2!")
            Case 2
                MessageBox.Show("You selected item #3!")
            Case 3
                MessageBox.Show("You selected item #4!")
            Case 4
                MessageBox.Show("You selected item #5!")
            Case 5
                MessageBox.Show("You selected item #6!")
            Case 6
                MessageBox.Show("You selected item #7!")
            Case 7
                MessageBox.Show("You selected item #8!")
            Case 8
                MessageBox.Show("You selected item #9!")
            Case 9
                MessageBox.Show("You selected item #10!")
        End Select
    End Sub

    Sub New()
        InitializeComponent()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.CacheText Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.FixedWidth Or
                 ControlStyles.SupportsTransparentBackColor Or ControlStyles.ResizeRedraw, True)
        DoubleBuffered = True
        TransparencyKey = Color.Fuchsia
        BackColor = TransparencyKey
        Text = MenuTitle
        Width = MenuWidth
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics = e.Graphics
        Dim ClientRectangle As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim HeaderRectangle As New Rectangle(0, 0, Width - 1, HEADER_HEIGHT)
        Dim SelectedItemRectangle As New Rectangle(0, HEADER_HEIGHT + (SelectedIndex * ITEM_HEIGHT), Width, ITEM_HEIGHT)
        G.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.Clear(TransparencyKey)

        G.FillRectangle(Brushes.Black, ClientRectangle)

        Dim Header1 As Color = ControlPaint.Dark(Color.FromArgb(0, 125, 255))
        Dim Header2 As Color = ControlPaint.Light(Color.FromArgb(0, 125, 255))

        Dim HeaderGrad As LinearGradientBrush = New LinearGradientBrush(HeaderRectangle, Header1, Header2, 80.0F)
        G.FillRectangle(HeaderGrad, HeaderRectangle)
        G.DrawString(MenuTitle, MenuFont, Brushes.White, HeaderRectangle, New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})

        G.FillRectangle(Brushes.White, SelectedItemRectangle)

        PopulateMenuItems()
        Height = HEADER_HEIGHT + (MenuItems.Count * ITEM_HEIGHT)

        For i As Integer = 0 To MenuItems.Count - 1
            Dim ItemRectangle As Rectangle = New Rectangle(0, HEADER_HEIGHT + (ITEM_HEIGHT * i), Width, ITEM_HEIGHT)
            Dim ItemColor As Color = Color.White
            If (SelectedIndex = i) Then
                ItemColor = Color.Black
            End If
            G.DrawString(MenuItems(i), MenuFont, New SolidBrush(ItemColor), ItemRectangle, New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        Next

        G.DrawRectangle(Pens.Black, ClientRectangle)
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim posX As Integer = e.Location.X
        Dim posY As Integer = e.Location.Y
        If (MenuMoving) Then
            Location = New Point(MousePosition.X - MouseP.X, MousePosition.Y - MouseP.Y)
        Else
            If (MenuItems.Count - 1 > 0) Then

                For y As Integer = 0 To MenuItems.Count - 1
                    Dim ItemStart As Integer = HEADER_HEIGHT + (y * ITEM_HEIGHT)
                    Dim ItemEnd As Integer = ItemStart + ITEM_HEIGHT
                    If posY >= ItemStart AndAlso posY <= ItemEnd Then
                        SelectedIndex = y
                    End If
                Next
                Invalidate()
            End If
        End If
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If ((e.Button = MouseButtons.Left) AndAlso (New Rectangle(0, 0, Width, HEADER_HEIGHT).Contains(e.Location))) Then
            MenuMoving = True
            MouseP = e.Location
        Else
            HandleMenuClick()
        End If
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        MenuMoving = False
    End Sub

End Class
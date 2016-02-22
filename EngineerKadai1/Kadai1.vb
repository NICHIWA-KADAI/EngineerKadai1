'以下をインポートする。
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.FileIO

Public Class frmKadai

    ''' <summary>
    ''' クラス定数
    ''' </summary>
    Private Const CON_CSV_OSAKA As String = "oosaka.csv"　　　'大阪CSVファイル名
    Private Const CON_CSV_TOKYO As String = "tokyo.csv"      '東京CSVファイル名
    Private Const CON_DEFULT_DELIMITER As String = ","        '区切り文字

    ''' <summary>
    ''' クラス変数
    ''' </summary>
    Private objCsvData As TextFieldParser
    Private dttOsakaData As Date()
    Private dttTokyoData As Date()

    ''' <summary>
    ''' フォームロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmKadai_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '画面初期化
        Me.dgvList.Rows.Clear()
    End Sub

    ''' <summary>
    ''' 実行ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmbOpen_Click(sender As Object, e As EventArgs) Handles cmbOpen.Click

        Try
            'CSVデータ取得
            If GetData() = False Then
                Exit Sub
            End If

            'グリッドビュー表示
            If SetGridView() = False Then
                Exit Sub
            End If

            'メッセージ表示
            Message.ShowMessage(4001)

            '例外処理
        Catch ex As Exception
            Message.ShowMessage(1001, vbCrLf + ex.Message)
        Finally
            'メモリ開放
            objCsvData = Nothing
            dttOsakaData = Nothing
            dttTokyoData = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' CSVファイルデータ取得
    ''' </summary>
    ''' <returns></returns>
    Private Function GetData() As Boolean

        Try
            '大阪CSVファイルデータ取得
            If GetOsakaCSV() = False Then
                Return False
            End If

            '東京CSVファイルデータ取得
            If GetTokyoCSV() = False Then
                Return False
            End If

            '戻り値設定
            Return True

            '例外処理
        Catch ex As Exception
            Message.ShowMessage(1001, vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 大阪CSVファイルデータ取得
    ''' </summary>
    ''' <returns></returns>
    Private Function GetOsakaCSV() As Boolean

        Try
            'CSVファイルオープン
            If OpenFile(CON_CSV_OSAKA) = False Then
                Return False
            End If

            'データ取得
            dttOsakaData = GetCsvData()

            'データ取得判定
            If dttOsakaData.Length = 0 Then
                Return False
            End If

            'CSVファイルクローズ
            If CloseFile() = False Then
                Return False
            End If

            '戻り値設定
            Return True

            '例外処理
        Catch ex As Exception
            Message.ShowMessage(1001, vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 東京CSVファイルデータ取得
    ''' </summary>
    ''' <returns></returns>
    Private Function GetTokyoCSV() As Boolean

        Try
            'CSVファイルオープン
            If OpenFile(CON_CSV_TOKYO) = False Then
                Return False
            End If

            'データ取得
            dttTokyoData = GetCsvData()

            'データ取得判定
            If dttTokyoData.Length = 0 Then
                Return False
            End If

            'CSVファイルクローズ
            If CloseFile() = False Then
                Return False
            End If

            '戻り値設定
            Return True

            '例外処理
        Catch ex As Exception
            Message.ShowMessage(1001, vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' CSVデータ取得関数
    ''' </summary>
    ''' <returns></returns>
    Private Function GetCsvData() As Date()

        Dim intCsvIdx As Integer = 0                   '配列インデックス
        Dim strTemData As String() = Nothing           'データ一時格納
        Dim strCurRow As String = Nothing              'CSVファイル取得値
        Dim dttTemData As Date                         '年月日
        Dim dblTemPreci As Double　　　                '降水量
        Dim dblTemHigh As Double                       '最高気温
        Dim dblTemLow As Double                        '最低気温
        Dim dttDate As Date() = Nothing　　　　    　　'判定データ格納

        Try
            Do Until objCsvData.EndOfData = -1

                'データ一行取得
                strCurRow = objCsvData.ReadLine

                'カンマ区切り分割
                strTemData = Regex.Split(strCurRow, CON_DEFULT_DELIMITER)

                '変数初期化
                dttTemData = Nothing
                dblTemPreci = Nothing
                dblTemHigh = Nothing
                dblTemLow = Nothing

                '取得データチェック
                If DataCheck(strTemData) = False Then
                    Continue Do
                End If

                '配列格納
                If Date.TryParse(strTemData(0), dttTemData) = True Then
                    dblTemPreci = CType(strTemData(1), Double)
                    '降水量が1以上の場合
                    If dblTemPreci >= 1 Then
                        dblTemHigh = CType(strTemData(3), Double)
                        dblTemLow = CType(strTemData(4), Double）
                        '最高気温と最低気温の差が1以上の場合
                        If dblTemHigh - dblTemLow >= 1 Then

                            '再定義
                            ReDim Preserve dttDate(intCsvIdx)
                            dttDate(intCsvIdx) = strTemData(0)
                            intCsvIdx = intCsvIdx + 1
                        Else
                            Continue Do
                        End If
                    Else
                        Continue Do
                    End If
                Else
                    Continue Do
                End If
            Loop

            '戻り値設定
            GetCsvData = dttDate

            '例外処理
        Catch ex As Exception
            Message.ShowMessage(1001, vbCrLf + ex.Message)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' ファイルオープン関数
    ''' </summary>
    ''' <param name="strFileName"></param>
    ''' <returns>True:正常 False:異常</returns>
    Private Function OpenFile(ByRef strFileName As String) As Boolean

        Try
            Dim strFilePath As String                                           'ファイルパス
            Dim strDirectory As String = IO.Directory.GetCurrentDirectory       'フォルダパス

            strFilePath = IO.Path.Combine(strDirectory, strFileName)

            'ファイル存在チェック
            If IO.File.Exists(strFilePath) = False Then
                Message.ShowMessage(2001)
                Return False
            End If

            objCsvData = New FileIO.TextFieldParser(strFilePath)

            With objCsvData
                .TextFieldType = FileIO.FieldType.Delimited
                .SetDelimiters(CON_DEFULT_DELIMITER)
            End With

            '戻り値設定
            Return True

            '例外処理
        Catch ex As Exception
            Message.ShowMessage(1001, vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' ファイルクローズ関数
    ''' </summary>
    ''' <returns>True:正常 False:異常</returns>
    Private Function CloseFile() As Boolean

        Try
            'ファイルクローズ
            objCsvData.Close()

            'メモリ解放
            objCsvData = Nothing

            '戻り値設定
            Return True

            '例外処理
        Catch ex As Exception
            Message.ShowMessage(1001, vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' グリッドビュー表示関数
    ''' </summary>
    ''' <returns>True:正常 False:異常</returns>
    Private Function SetGridView() As Boolean

        Dim intOsakaIdx As Integer       '大阪データインデックス
        Dim intTokyoIdx As Integer       '東京データインデックス
        Dim intDateIdx As Integer = 0    '比較データインデックス

        Try
            '大阪と東京で同じ日付をグリッドビューにセット
            For intOsakaIdx = 0 To ArrayCount(dttOsakaData)
                For intTokyoIdx = 0 To ArrayCount(dttTokyoData)
                    If Date.Compare(dttOsakaData(intOsakaIdx), dttTokyoData(intTokyoIdx)) = 0 Then
                        With Me.dgvList
                            .Rows.Add()
                            .Rows(intDateIdx).Cells(0).Value = dttOsakaData(intOsakaIdx).ToShortDateString
                            .Rows(intDateIdx).HeaderCell.Value = (intDateIdx + 1).ToString()
                            intDateIdx = intDateIdx + 1
                            .AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders)
                        End With
                        Exit For
                    End If
                Next
            Next

            '戻り値設定
            Return True

            '例外処理
        Catch ex As Exception
            Message.ShowMessage(1001, vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 配列数カウント
    ''' </summary>
    ''' <param name="dttDate"></param>
    ''' <returns></returns>
    Private Function ArrayCount(ByRef dttDate As Date()) As Integer
        Try
            Return dttDate.Length - 1
        Catch
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' CSVファイルデータチェック
    ''' </summary>
    ''' <param name="strTemData"></param>
    ''' <returns></returns>
    Private Function DataCheck(ByVal strTemData As String()) As Boolean

        Try
            '空白チェック
            If strTemData(0).ToString.Length = 0 Then
                Return False
            End If

            '配列数チェック
            If strTemData.Length <> 5 Then
                Return False
            End If

            '戻り値設定
            Return True

            '例外処理
        Catch ex As Exception
            Message.ShowMessage(1001, vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 終了ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmbExit_Click(sender As Object, e As EventArgs) Handles cmbExit.Click

        Try
            'メッセージ表示
            If Message.ShowMessage(3001) = MsgBoxResult.Cancel Then
                Exit Sub
            End If

            'メモリ開放
            objCsvData = Nothing
            dttOsakaData = Nothing
            dttTokyoData = Nothing

            '閉じる
            Me.Close()

            '例外処理
        Catch ex As Exception
            Message.ShowMessage(1001, vbCrLf + ex.Message)
        End Try
    End Sub
End Class

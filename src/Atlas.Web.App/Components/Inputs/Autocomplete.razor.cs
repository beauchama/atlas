// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;

namespace Atlas.Web.App.Components.Inputs;

public sealed partial class Autocomplete
{
    private const int TriggerSearchLength = 3;

    private ElementReference _autocomplete;
    private string? _searchValue;
    private IEnumerable<KeyValuePair<string, string>> _filteredItems = [];

    [Parameter, EditorRequired]
    public required string Value { get; init; }

    [Parameter]
    public EventCallback<string> ValueChanged { get; init; }

    [Parameter, EditorRequired]
    public required IReadOnlyDictionary<string, string> Items { get; init; }

    [Parameter]
    public string Placeholder { get; init; } = string.Empty;

    [Parameter]
    public string Css { get; init; } = string.Empty;

    private string ExpandedCss => _filteredItems.Any() ? "expanded" : string.Empty;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        await _autocomplete.FocusAsync();
    }

    private void ExpandDropdown() => _filteredItems = Search(_searchValue);

    private void SearchItems(ChangeEventArgs args)
    {
        _searchValue = args.Value?.ToString();

        _filteredItems = _searchValue switch
        {
            { Length: >= TriggerSearchLength } => Search(_searchValue),
            { Length: < TriggerSearchLength } or _ => Items
        };
    }

    private IEnumerable<KeyValuePair<string, string>> Search(string? searchValue)
    {
        return searchValue is null or { Length: < TriggerSearchLength }
            ? Items
            : Items.Where(i => i.Value.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
    }

    private Task HandleKeyboardAsync(KeyboardEventArgs args)
    {
        if (IsEnterPressed(args.Code) && _filteredItems.TryFirstKey(out string? key))
        {
            Reset();
            return SelectItemAsync(key);
        }

        if (args.Code.Equals(KeyboardCodes.Escape, StringComparison.Ordinal))
            Reset();

        return Task.CompletedTask;

        static bool IsEnterPressed(string code)
        {
            return code.Equals(KeyboardCodes.Enter, StringComparison.Ordinal)
                || code.Equals(KeyboardCodes.NumpadEnter, StringComparison.Ordinal);
        }
    }

    private void CollapseDropdown() => Reset();

    private Task SelectItemAsync(string value)
    {
        Reset();
        return ValueChanged.InvokeAsync(value);
    }

    private void Reset()
    {
        _searchValue = null;
        _filteredItems = [];
    }
}

file static class EnumerableExtensions
{
    internal static bool TryFirstKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, [NotNullWhen(true)] out TKey? key)
    {
        using IEnumerator<KeyValuePair<TKey, TValue>> enumerator = source.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            key = default;
            return false;
        }

        key = enumerator.Current.Key;
        return key is not null;
    }
}

interface JQueryStatic {
    hash(): JQueryHashPlugin;
}

interface JQueryHashPlugin {
    get(): string;
    get(query: string): string;
}

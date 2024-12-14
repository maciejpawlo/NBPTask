export interface Column<T> {
  displayName: string,
  key: keyof T
}

import { Component, AfterViewInit, ViewChild, input, effect, OnInit } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Column } from './models/column';
@Component({
  selector: 'app-table',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css'
})
export class TableComponent<T> implements AfterViewInit {
  data = input.required<T[]>();
  columns = input.required<Column<T>[]>();
  dataSource = new MatTableDataSource<T>([]);

  constructor(){
    effect(() => {
      this.dataSource.data = this.data();
    })
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  ngAfterViewInit() {
    //NOTE: in production scenario we could use i8n translations there
    this.paginator._intl.itemsPerPageLabel = 'Ilość elementow na stronie: ';
    this.dataSource.paginator = this.paginator;
  }

  getColumnNames(): string[] {
    return this.columns().map((x) => x.displayName);
  }
}

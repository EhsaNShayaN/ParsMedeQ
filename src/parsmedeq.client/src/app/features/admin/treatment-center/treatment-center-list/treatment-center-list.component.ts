import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {BaseComponent} from '../../../../base-component';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {tap} from 'rxjs';
import {Helpers} from '../../../../core/helpers';
import {AddResult, BaseResult} from '../../../../core/models/BaseResult';
import {ToastrService} from 'ngx-toastr';
import {MatTableDataSource} from '@angular/material/table';
import {TreatmentCenter, TreatmentCenterResponse} from '../../../../core/models/TreatmentCenterResponse';
import {Location, LocationResponse} from '../../../../core/models/LocationResponse';

@Component({
  selector: 'app-treatment-center-list',
  styleUrls: ['treatment-center-list.component.scss'],
  templateUrl: 'treatment-center-list.component.html',
  standalone: false
})
export class TreatmentCenterListComponent extends BaseComponent implements OnInit, AfterViewInit {
  columnsToDisplay: string[] = [/*'row', */'title', 'province', 'city', 'image', 'creationDate', 'actions'];
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  locations: Location[] = [];
  ///////////
  dataSource!: MatTableDataSource<TreatmentCenter>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, {static: true}) sort: MatSort | null = null;
  pageIndex = 1;
  pageSize = 5;
  totalCount = 0;

  constructor(private helpers: Helpers,
              private toastrService: ToastrService,) {
    super();
    this.languages = this.translateService.getLangs();
  }

  ngOnInit(): void {
    this.helpers.setPaginationLang();
    this.restApiService.getLocations().subscribe((acr: LocationResponse) => {
      this.locations = acr.data;
      this.getItems();
    });
  }

  getItems() {
    let model = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sort: 0,
    };
    this.restApiService.getTreatmentCenters(model).subscribe((res: TreatmentCenterResponse) => {
      if (res?.data) {
        res.data.items.forEach(item => {
          item.province = this.locations.find(s => s.id === item.provinceId)?.title ?? '';
          item.city = this.locations.find(s => s.id === item.cityId)?.title ?? '';
        })
        this.initDataSource(res);
      }
    });
  }

  initDataSource(res: TreatmentCenterResponse) {
    this.totalCount = res.data.totalCount;
    this.pageSize = res.data.pageSize;
    this.dataSource = new MatTableDataSource<TreatmentCenter>(res.data.items);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngAfterViewInit() {
    this.paginator?.page
      .pipe(
        tap(() => this.getItems())
      )
      .subscribe();
  }

  remove(item: any) {
    const index: number = this.dataSource.data.indexOf(item);
    if (index !== -1) {
      let dialogRef = this.dialogService.openConfirmDialog('', this.getTranslateValue('SURE_DELETE'));
      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.restApiService.deleteTreatmentCenter(item.id).subscribe({
            next: (response: BaseResult<AddResult>) => {
              if (response.data.changed) {
                this.dataSource.data.splice(index, 1);
                this.dataSource._updateChangeSubscription();
                this.toastrService.success(this.getTranslateValue('ITEM_DELETED_SUCCESSFULLY'), '', {});
              } else {
                this.toastrService.error(this.getTranslateValue('THIS_ITEM_CANNOT_BE_DELETED_PLEASE_DELETE_ITS_SUBCATEGORIES_FIRST'), '', {});
              }
            }
          });
        }
      });
    }
  }

  getColName(column: string) {
    column = column.toUpperCase();
    if (column === 'PARENTID') {
      column = 'CATEGORY';
    }
    if (column === 'CREATIONDATE') {
      column = 'CREATION_DATE';
    }
    return this.getTranslateValue(column);
  }
}

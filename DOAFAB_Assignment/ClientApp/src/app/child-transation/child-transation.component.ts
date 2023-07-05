import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpService } from '../Service/http.service';

@Component({
  selector: 'app-child-transation',
  templateUrl: './child-transation.component.html',
  styleUrls: ['./child-transation.component.css']
})
export class ChildTransationComponent {
  transactions: any = [];
  pageNumber = 0;
  pageSize = 2;
  parentId = 0;
  totalRows = 0;
  parentData: any;
  constructor(private route: ActivatedRoute, private router: Router, private httpService: HttpService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.parentId = params['id'];
      this.pageNumber++;
      this.getData();
    });
  }
  getData() {
    this.httpService.get("https://localhost:7005/api/Payment/GetTransactionDetails" + "?pageSize=" + this.pageSize + "&pageNumber=" + this.pageNumber + "&parentId=" + this.parentId).subscribe({
      next: (data: any) => {
        this.transactions = data.resultData;
        this.totalRows = data.totalRows;
        this.parentData = data.parentData;
      },
      error: (e) => console.error(e)
    });
  }
  navigateToTransactions() {
    this.router.navigate(['/transactions']);
  }
  loadPage(data: any) {
    this.pageNumber = data;
    this.getData();
  }
}
